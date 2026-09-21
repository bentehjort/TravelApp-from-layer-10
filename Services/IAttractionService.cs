using Models;
using Models.DTO;

namespace Services;

public interface IAttractionService
{
    public Task<ResponsePageDto<IAttraction>> ReadAttractionsAsync();
    public Task<ResponseItemDto<IAttraction>> ReadAttractionAsync(Guid id);
}
