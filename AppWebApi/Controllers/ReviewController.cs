using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

using Services;
using Configuration;
using Configuration.Options;
using Microsoft.Extensions.Options;
using Models.DTO;
using Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReviewController : Controller
    {
        readonly IReviewService _service = null;
        readonly ILogger<ReviewController> _logger = null;
        [HttpDelete("{id}")]
        [ActionName("DeleteReview")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<ReviewDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteReview)}: {nameof(id)}");
                var resp = await _service.DeleteReview(id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteReview)}: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }
        [HttpPost()]
        [ActionName("CreateReview")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<ReviewDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateReviewAsync(ReviewCuDto itemDto)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateReviewAsync)}:");
                var resp = await _service.CreateReviewAsync(itemDto);
                _logger.LogInformation($"item {resp.Item.ReviewId} created");
                return Ok(resp);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(CreateReviewAsync)}: {ex.Message}");
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }
        public ReviewController(IReviewService service, ILogger<ReviewController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}