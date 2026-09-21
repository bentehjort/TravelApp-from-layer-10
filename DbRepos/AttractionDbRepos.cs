using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using DbContext;
using Models.DTO;

namespace DbRepos;

public class AttractionDbRepos
{
    private ILogger<AttractionDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public AttractionDbRepos(ILogger<AttractionDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
     public async Task<ResponsePageDto<IAttraction>> ReadAttractionsAsync()
    {
        IQueryable<AttractionDbM> query = _dbContext.Attractions.AsNoTracking();
        var ret = new ResponsePageDto<IAttraction>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.ToListAsync<IAttraction>(),
        };
        return ret;
    }
    public async Task<ResponseItemDto<IAttraction>> ReadAttractionAsync(Guid id)
    {
        IQueryable<AttractionDbM> query = _dbContext.Attractions
        .Include(a=> a.CityDbM).ThenInclude(c => c.CountryDbM)
        .Include(a=> a.ReviewDbMs).ThenInclude(r => r.UserDbM)
        .AsNoTracking()
        .Where(a => a.AttractionId == id);

        var item = await query.FirstOrDefaultAsync<IAttraction>();
        return new ResponseItemDto<IAttraction>
        {
#if DEBUG
        ConnectionString = _dbContext.dbConnection,
#endif
        Item = item,
        };
    }
}