using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using DbContext;
using Models.DTO;

namespace DbRepos;

public class ReviewDbRepos
{
    private ILogger<ReviewDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    public async Task<ResponseItemDto<ReviewDto>> DeleteReview(Guid id)
    {
        var query = _dbContext.Reviews
        .Where(r => r.ReviewId == id);
        var item = await query.FirstOrDefaultAsync<ReviewDbM>();
        if (item == null) throw new ArgumentException($"Item {id} does not exist in table.");

        _dbContext.Remove(item);

        await _dbContext.SaveChangesAsync();
        return new ResponseItemDto<ReviewDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = new ReviewDto(item)
        };
    }
    public async Task<ResponseItemDto<ReviewDto>> CreateReviewAsync(ReviewCuDto itemDto)
    {
        if (itemDto.ReviewId != null)
            throw new ArgumentException($"{nameof(itemDto.ReviewId)} must be null when creating a new object");

        var item = new ReviewDbM(itemDto);

        await navProp_ReviewCuDto_to_ReviewDbM(itemDto, item);

        _dbContext.Reviews.Add(item);

        await _dbContext.SaveChangesAsync();
                return new ResponseItemDto<ReviewDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = new ReviewDto(item)
        };

    }
    private async Task navProp_ReviewCuDto_to_ReviewDbM(ReviewCuDto itemDtoSrc, ReviewDbM itemDst)
    {
    // to map attraction
       if (itemDtoSrc.AttractionId != null)
    {
        // find attraction in database
        itemDst.AttractionDbM = await _dbContext.Attractions
            .FirstOrDefaultAsync(a => a.AttractionId == itemDtoSrc.AttractionId);

        // throw exception if does not exist in database
        if (itemDst.AttractionDbM == null)
            throw new ArgumentException($"Attraction with ID {itemDtoSrc.AttractionId} does not exist.");
    }
    else
    {
        throw new ArgumentException("A review must be connected to an Attraction (AttractionId cannot be null).");
    }

    //map user
    if (itemDtoSrc.UserId != null)
    {
        itemDst.UserDbM = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserId == itemDtoSrc.UserId);

        if (itemDst.UserDbM == null)
            throw new ArgumentException($"User with ID {itemDtoSrc.UserId} does not exist.");
    }
    else
    {
        throw new ArgumentException("A review must be connected to a User (UserId cannot be null).");
    }
    }
    public ReviewDbRepos(ILogger<ReviewDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
}