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
    public class AttractionController : Controller
    {
        readonly IAttractionService _service = null;
        readonly ILogger<AttractionController> _logger = null;
        [HttpGet()]
        [ActionName("Read")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read()
        {
            try
            {
                _logger.LogInformation($"{nameof(Read)}");
                var resp = await _service.ReadAttractionsAsync();     
                return Ok(resp);     
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet()]
        [ActionName("ReadItem")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(Read)}");
                var resp = await _service.ReadAttractionAsync(id);     
                return Ok(resp);     
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        public AttractionController(IAttractionService service, ILogger<AttractionController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}
