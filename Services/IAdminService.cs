using Microsoft.EntityFrameworkCore.Storage;
using Models.DTO;

namespace Services;

public interface IAdminService
{
    public Task SeedAsync(int nrItems);
    public Task RemoveSeedAsync(bool seeded);
    public Task<ResponseItemDto<DatabaseCountedDto>> CountInfoAsync();
     public Task<ResponseItemDto<DatabaseCountedDto>> RemoveSeedWithStoredProcedure(bool seeded);
}
