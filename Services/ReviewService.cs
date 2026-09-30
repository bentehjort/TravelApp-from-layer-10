using Microsoft.Extensions.Logging;

using Models;
using DbRepos;
using Models.DTO;

namespace Services;

public class ReviewService : IReviewService
{
    private readonly ReviewDbRepos _reviewDbRepos;
    private readonly ILogger<ReviewService> _logger;
    public ReviewService(ReviewDbRepos reviewDbRepos, ILogger<ReviewService> logger)
    {
        _reviewDbRepos = reviewDbRepos;
        _logger = logger;
    }
    public Task<ResponseItemDto<ReviewDto>> DeleteReview(Guid id) => _reviewDbRepos.DeleteReview(id);
    public Task<ResponseItemDto<ReviewDto>> CreateReviewAsync(ReviewCuDto itemDto)=> _reviewDbRepos.CreateReviewAsync(itemDto);

}