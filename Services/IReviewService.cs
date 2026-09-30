using Microsoft.EntityFrameworkCore.Storage;
using Models.DTO;
using Models;

namespace Services;

public interface IReviewService
{
    public Task<ResponseItemDto<ReviewDto>> DeleteReview(Guid id);
    public Task<ResponseItemDto<ReviewDto>> CreateReviewAsync(ReviewCuDto itemDto);
}