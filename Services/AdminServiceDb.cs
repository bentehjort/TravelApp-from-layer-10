using Microsoft.Extensions.Logging;

using DbRepos;
using Models.DTO;

namespace Services;
    
public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;
    private readonly ILogger<AdminServiceDb> _logger = null;

    public Task SeedAsync(int nrItems) => _repo.SeedAsync(nrItems);
    public Task RemoveSeedAsync(bool seeded) => _repo.RemoveSeedAsync(seeded);
    public Task<ResponseItemDto<DatabaseCountedDto>> CountInfoAsync()=> _repo.CountInfoAsync();
    public Task<ResponseItemDto<DatabaseCountedDto>> RemoveSeedWithStoredProcedure(bool seeded) => _repo.RemoveSeedWithStoredProcedure(seeded);

    #region constructors
    public AdminServiceDb(AdminDbRepos repo)
    {
        _repo = repo;
    }
    public AdminServiceDb(AdminDbRepos repo, ILogger<AdminServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

