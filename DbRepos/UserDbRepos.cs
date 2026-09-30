using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using DbContext;
using Models.DTO;

namespace DbRepos;

public class UserDbRepos
{
    private ILogger<UserDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public async Task<ResponsePageDto<UserDto>> ReadUsersAndReviewsAsync(int pageSize, int pageNumber)
    {
        IQueryable<UserDbM> query = _dbContext.Users.AsNoTracking()
        .Include(u => u.ReviewDbMs);
        var dbItems = await query
        //Adding paging
        .Skip(pageNumber * pageSize)
        .Take(pageSize)
        .ToListAsync();

        var dbList = dbItems.Select(item => new UserDto(item)).ToList();
        var ret = new ResponsePageDto<UserDto>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query.CountAsync(),
            PageItems = dbList
,

            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }
    public async Task<ResponseItemDto<UserDto>> DeleteUser(Guid id)
    {
        //Finding correct user by id, including relations
        var query = _dbContext.Users
        .Where(u => u.UserId == id)
        .Include(u => u.ReviewDbMs);
        var item = await query.FirstOrDefaultAsync<UserDbM>();
        //throw exception if no matching id could be found
        if (item == null) throw new ArgumentException($"Item {id} does not exist in table.");
        //delete object from database model 
        _dbContext.Remove(item);
        //write changes to database
        await _dbContext.SaveChangesAsync();
        return new ResponseItemDto<UserDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = new UserDto(item)
        };
    }
    public async Task<ResponseItemDto<UserDto>> CreateUserAsync(UserCuDto itemDto)
    {
        if (itemDto.UserId != null)
            throw new ArgumentException($"{nameof(itemDto.UserId)} must be null when creating a new object");

        var item = new UserDbM(itemDto);
        _dbContext.Users.Add(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<UserDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = new UserDto(item)
        };

    }
    public UserDbRepos(ILogger<UserDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
}