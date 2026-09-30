using Models;
using Models.DTO;

namespace Services;

public interface IAttractionService
{
    public Task<ResponsePageDto<AttractionDto>> ReadAttractionsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<AttractionDto>> ReadAttractionAsync(Guid id);
    public Task<ResponsePageDto<AttractionDto>> ReadAttractionsWithNoReviewsAsync(int pageSize, int pageNumber);
    public Task<ResponseItemDto<IAttraction>> DeleteAttraction(Guid id);
    public Task<ResponseItemDto<AttractionDto>> UpdateAttractionAsync(AttractionCuDto itemDto);
    public Task<ResponseItemDto<AttractionDto>> CreateAttractionAsync(AttractionCuDto itemDto);
}
