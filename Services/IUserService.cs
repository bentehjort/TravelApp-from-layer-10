using Models;
using Models.DTO;

namespace Services;

public interface IUserService
{
    public Task<ResponsePageDto<UserDto>> ReadUsersAndReviewsAsync(int pageSize, int pageNumber);
    public Task<ResponseItemDto<UserDto>> DeleteUser(Guid id);
    public Task<ResponseItemDto<UserDto>> CreateUserAsync(UserCuDto itemDto);
}