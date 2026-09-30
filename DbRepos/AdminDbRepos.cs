using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbContext;
using Configuration;
using Models.DTO;
using Microsoft.Data.SqlClient;


namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    //method to fetch view and return in our DatabaseCountedDto
    public async Task<ResponseItemDto<DatabaseCountedDto>> CountInfoAsync()
    {
        var info = await _dbContext.DatabaseCountedView.FirstOrDefaultAsync();

        return new ResponseItemDto<DatabaseCountedDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = info
        };
    }
    public async Task SeedAsync(int nrItems)
    {
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        //remove existing quotes in the database
        var countries = seeder.ItemsToList<CountryDbM>(5);

        var cities = seeder.ItemsToList<CityDbM>(100);
        foreach (var city in cities)
        {
            city.CountryDbM = seeder.FromList<CountryDbM>(countries);
            //Testar lägga till för att knyta stad till rätt land
            city.CityName = seeder.City(city.CountryDbM.CountryName);
        }

        var attractions = seeder.ItemsToList<AttractionDbM>(1000);
        foreach (var attraction in attractions)
        {
            attraction.CityDbM = seeder.FromList<CityDbM>(cities);
        }

        var users = seeder.ItemsToList<UserDbM>(50);


        foreach (var attraction in attractions)
        {
            int numberOfReviews = seeder.Next(0, 21);
            var attractionReviews = seeder.ItemsToList<ReviewDbM>(numberOfReviews);
            foreach (var review in attractionReviews)
            {
                review.UserDbM = seeder.FromList<UserDbM>(users);
            }

            attraction.ReviewDbMs = attractionReviews;

        }





        _dbContext.Countries.AddRange(countries);
        _dbContext.Cities.AddRange(cities);
        _dbContext.Attractions.AddRange(attractions);
        _dbContext.Users.AddRange(users);
        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }
    //Remove seed with stored procedure

    public async Task<ResponseItemDto<DatabaseCountedDto>> RemoveSeedWithStoredProcedure(bool seeded)
    {
        var connection = _dbContext.Database.GetDbConnection();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "spDeleteAllSeed";
        var sqlParam = new SqlParameter("seededParam", seeded);
        command.Parameters.Add(sqlParam);

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();
        DatabaseCountedDto result_set = null;
        if (reader.HasRows)
        {
            await reader.ReadAsync();

            result_set = new DatabaseCountedDto
            {
                NrSeededAttractions = Convert.ToInt32(reader["NrSeededAttractions"]),
                NrUnseededAttractions = Convert.ToInt32(reader["NrUnseededAttractions"]),
                NrSeededCountries = Convert.ToInt32(reader["NrSeededCountries"]),
                NrUnseededCountries = Convert.ToInt32(reader["NrUnseededCountries"]),
                NrSeededCities = Convert.ToInt32(reader["NrSeededCities"]),
                NrUnseededCities = Convert.ToInt32(reader["NrUnseededCities"]),
                NrSeededReviews = Convert.ToInt32(reader["NrSeededReviews"]),
                NrUnseededReviews = Convert.ToInt32(reader["NrUnseededReviews"]),
                NrSeededUsers = Convert.ToInt32(reader["NrSeededUsers"]),
                NrUnseededUsers = Convert.ToInt32(reader["NrUnseededUsers"])
            };
        }
        await reader.CloseAsync();
        return new ResponseItemDto<DatabaseCountedDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = result_set
        };
    }
    
    public async Task RemoveSeedAsync(bool seeded)
    {
        _dbContext.Countries.RemoveRange(_dbContext.Countries.Where(co => co.Seeded == seeded));
        _dbContext.Cities.RemoveRange(_dbContext.Cities.Where(ci => ci.Seeded == seeded));
        _dbContext.Attractions.RemoveRange(_dbContext.Attractions.Where(at => at.Seeded == seeded));
        _dbContext.Users.RemoveRange(_dbContext.Users.Where(us => us.Seeded == seeded));
        await _dbContext.SaveChangesAsync();
    }


    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
