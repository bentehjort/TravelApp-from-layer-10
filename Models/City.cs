using Seido.Utilities.SeedGenerator;
namespace Models;
public class City : ICity, ISeed<City>
{
    public virtual Guid CityId {get; set;}
    public virtual string CityName {get; set;}
    public virtual Country Country {get; set;}
    public virtual List<IAttraction> Attractions {get; set;} = null;
    public bool Seeded {get; set;} = false;
    public City Seed (SeedGenerator seeder)
    {
        Seeded = true;
        CityId = Guid.NewGuid();
        CityName = seeder.City();
        return this;
    }
}