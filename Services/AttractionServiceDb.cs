using Microsoft.Extensions.Logging;

using Models;
using DbRepos;
using Models.DTO;

namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _attractionDbRepos;
    private readonly ILogger<AttractionServiceDb> _logger;
    public AttractionServiceDb(AttractionDbRepos attractionDbRepos, ILogger<AttractionServiceDb> logger)
    {
        _attractionDbRepos = attractionDbRepos;
        _logger = logger;
    }
    public Task<ResponsePageDto<AttractionDto>> ReadAttractionsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _attractionDbRepos.ReadAttractionsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<AttractionDto>> ReadAttractionAsync(Guid id) => _attractionDbRepos.ReadAttractionAsync(id);
    public Task<ResponsePageDto<AttractionDto>> ReadAttractionsWithNoReviewsAsync(int pageSize, int pageNumber) => _attractionDbRepos.ReadAttractionsWithNoReviewsAsync(pageSize, pageNumber);
    public Task<ResponseItemDto<IAttraction>> DeleteAttraction(Guid id)=> _attractionDbRepos.DeleteAttraction(id);
    public Task<ResponseItemDto<AttractionDto>> UpdateAttractionAsync(AttractionCuDto itemDto) => _attractionDbRepos.UpdateAttractionAsync(itemDto);
    public Task<ResponseItemDto<AttractionDto>> CreateAttractionAsync(AttractionCuDto itemDto)=> _attractionDbRepos.CreateAttractionAsync(itemDto);
}