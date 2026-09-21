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
    public Task<ResponsePageDto<IAttraction>> ReadAttractionsAsync() => _attractionDbRepos.ReadAttractionsAsync();
    public Task<ResponseItemDto<IAttraction>> ReadAttractionAsync(Guid id) => _attractionDbRepos.ReadAttractionAsync(id);
}