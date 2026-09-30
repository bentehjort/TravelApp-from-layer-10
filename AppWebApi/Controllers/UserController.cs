using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

using Models;
using Services;
using Microsoft.AspNetCore.Authorization;
using Models.DTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        readonly IUserService _service = null;
        readonly ILogger<UserController> _logger = null;
        [HttpGet()]
        [ActionName("ReadUsers")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<UserDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        //Fix this method, maybe remove id, not readable
        public async Task<IActionResult> ReadUsers(int pageSize = 10, int pageNumber = 0)
        {
            try
            {
                _logger.LogInformation($"{nameof(ReadUsers)}: {nameof(pageSize)}: {nameof(pageNumber)}");
                var resp = await _service.ReadUsersAndReviewsAsync(pageSize, pageNumber);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadUsers)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteUser")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<UserDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteUser)}: {nameof(id)}");
                var resp = await _service.DeleteUser(id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteUser)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost()]
        [ActionName("CreateUser")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<UserDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateUserAsync(UserCuDto itemDto)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateUserAsync)}:");
                var resp = await _service.CreateUserAsync(itemDto);
                _logger.LogInformation($"item {resp.Item.UserId} created");
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateUserAsync)}: {ex.Message}");
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }
        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}