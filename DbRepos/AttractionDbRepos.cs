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
    public async Task<ResponsePageDto<AttractionDto>> ReadAttractionsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        filter = filter.ToLower();
        IQueryable<AttractionDbM> query;
        if (flat)
        {
            query = _dbContext.Attractions.AsNoTracking();
        }
        else
        {
            query = _dbContext.Attractions.AsNoTracking()
            .Include(a => a.CityDbM)
            .ThenInclude(c => c.CountryDbM);
        }
        //adding filtering directly to the query, so that we only get the items we want from the database
        query = query.Where(i => (i.Seeded == seeded) &&
                (i.AttractionName.ToLower().Contains(filter) ||
                 i.AttractionDescription.ToLower().Contains(filter) ||
                 i.AttractionCategory.ToLower().Contains(filter) ||
                 i.CityDbM.CityName.ToLower().Contains(filter) ||
                 i.CityDbM.CountryDbM.CountryName.ToLower().Contains(filter)));

        var dbItems = await query
        //Adding paging
        .Skip(pageNumber * pageSize)
        .Take(pageSize)
        .ToListAsync();

        var dbList = dbItems.Select(item => new AttractionDto(item)).ToList();
        var ret = new ResponsePageDto<AttractionDto>()
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
    public async Task<ResponseItemDto<AttractionDto>> ReadAttractionAsync(Guid id)
    {
        //returns attraction name, description, category and list of reviews
        IQueryable<AttractionDbM> query = _dbContext.Attractions
        .Include(a => a.ReviewDbMs)
        .AsNoTracking()
        .Where(a => a.AttractionId == id);

        var item = await query.FirstOrDefaultAsync();
        return new ResponseItemDto<AttractionDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = new AttractionDto(item),
        };
    }
    //To delete attraction and its reviews
    //Chose to return dto IAttraction to show which reviews were removed, as the AttractionDTO does not include reviews in its presentation
    public async Task<ResponseItemDto<IAttraction>> DeleteAttraction(Guid id)
    {
        var query = _dbContext.Attractions
        .Where(a => a.AttractionId == id)
        .Include(a => a.ReviewDbMs);
        var item = await query.FirstOrDefaultAsync<AttractionDbM>();
        if (item == null) throw new ArgumentException($"Item {id} does not exist in table.");
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<IAttraction>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }
    public async Task<ResponsePageDto<AttractionDto>> ReadAttractionsWithNoReviewsAsync(int pageSize, int pageNumber)
    {
        IQueryable<AttractionDbM> query = _dbContext.Attractions.AsNoTracking()
        .Where(a => a.ReviewDbMs.Count == 0)
        .Include(a => a.CityDbM)
        .ThenInclude(c => c.CountryDbM);
        var dbItems = await query
        //Adding paging
        .Skip(pageNumber * pageSize)
        .Take(pageSize)
        .ToListAsync();

        var dbList = dbItems.Select(item => new AttractionDto(item)).ToList();
        var ret = new ResponsePageDto<AttractionDto>()
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
    public async Task<ResponseItemDto<AttractionDto>> CreateAttractionAsync(AttractionCuDto itemDto)
    {
        if (itemDto.AttractionId != null)
            throw new ArgumentException($"{nameof(itemDto.AttractionId)} must be null when creating a new object");

        var item = new AttractionDbM(itemDto);

        await navProp_AttractionCuDto_to_AttractionDbM(itemDto, item);

        _dbContext.Attractions.Add(item);

        await _dbContext.SaveChangesAsync();
        return new ResponseItemDto<AttractionDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = new AttractionDto(item)
        };


    }

    public async Task<ResponseItemDto<AttractionDto>> UpdateAttractionAsync(AttractionCuDto itemDto)
    {
        var query = _dbContext.Attractions
        .Where(a => a.AttractionId == itemDto.AttractionId);
        var item = await query
        .Include(a => a.CityDbM)
        .ThenInclude(c => c.CountryDbM)
        .Include(a => a.ReviewDbMs)
        .FirstOrDefaultAsync<AttractionDbM>();

        //If the item does not exists
        if (item == null) throw new ArgumentException($"Item {itemDto.AttractionId} is not existing");

        item.UpdateFromDTO(itemDto);

        await navProp_AttractionCuDto_to_AttractionDbM(itemDto, item);

        _dbContext.Attractions.Update(item);

        await _dbContext.SaveChangesAsync();

        return new ResponseItemDto<AttractionDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = new AttractionDto(item)
        };


    }
    private async Task navProp_AttractionCuDto_to_AttractionDbM(AttractionCuDto itemDtoSrc, AttractionDbM itemDst)
    {
        if (itemDtoSrc.CityId != null)
        {
            itemDst.CityDbM = await _dbContext.Cities
                .Include(c => c.CountryDbM)
                .FirstOrDefaultAsync(a => a.CityId == itemDtoSrc.CityId);

            // added to make sure we do not add non existing city 
            if (itemDst.CityDbM == null)
                throw new ArgumentException($"City with ID {itemDtoSrc.CityId} does not exist.");
        }
        else
        {
             itemDst.CityDbM = null;
        }
    }

}