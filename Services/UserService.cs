using Microsoft.Extensions.Logging;

using Models;
using DbRepos;
using Models.DTO;

namespace Services;

public class UserServiceDb : IUserService
{
    private readonly UserDbRepos _userDbRepos;
    private readonly ILogger<UserServiceDb> _logger;
    public UserServiceDb(UserDbRepos userDbRepos, ILogger<UserServiceDb> logger)
    {
        _userDbRepos = userDbRepos;
        _logger = logger;
    }
    public Task<ResponsePageDto<UserDto>> ReadUsersAndReviewsAsync(int pageSize, int pageNumber)=> _userDbRepos.ReadUsersAndReviewsAsync(pageSize, pageNumber);
    public Task<ResponseItemDto<UserDto>> DeleteUser(Guid id)=> _userDbRepos.DeleteUser(id);
    public Task<ResponseItemDto<UserDto>> CreateUserAsync(UserCuDto itemDto)=> _userDbRepos.CreateUserAsync(itemDto);
}