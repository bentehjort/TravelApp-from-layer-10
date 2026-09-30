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
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<AttractionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> Read(string seeded = "true", string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool seededArg = bool.Parse(seeded);
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSizeArg = int.Parse(pageSize);
                _logger.LogInformation($"{nameof(Read)}: {nameof(seededArg)}: {seededArg}, {nameof(flatArg)}: {flatArg}, " +
                $"{nameof(pageNrArg)}: {pageNrArg}, {nameof(pageSizeArg)}: {pageSizeArg}");

                var resp = await _service.ReadAttractionsAsync(seededArg, flatArg, filter, pageNrArg, pageSizeArg);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(Read)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ActionName("ReadItem")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<AttractionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(ReadItem)}");
                var resp = await _service.ReadAttractionAsync(id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItem)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteAttraction")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IAttraction>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteAttraction(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteAttraction)}: {nameof(id)}");
                var resp = await _service.DeleteAttraction(id);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteAttraction)}: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpGet()]
        [ActionName("ReadAttractionWoReviews")]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<AttractionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadAttractionWoReviews(int pageSize = 10, int pageNumber = 0)
        {
            try
            {
                _logger.LogInformation($"{nameof(ReadAttractionWoReviews)}: {nameof(pageSize)}: {nameof(pageNumber)}");
                var resp = await _service.ReadAttractionsWithNoReviewsAsync(pageSize, pageNumber);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadAttractionWoReviews)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ActionName("UpdateAttraction")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<AttractionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateAttraction(string id, [FromBody] AttractionCuDto item)
        {
            try
            {
                var idArg = Guid.Parse(id);
                _logger.LogInformation($"{nameof(UpdateAttraction)}: {nameof(idArg)}: {idArg}");
                if (item.AttractionId != idArg) throw new ArgumentException("Id mismatch");

                var resp = await _service.UpdateAttractionAsync(item);
                _logger.LogInformation($"item {idArg} updated");

                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateAttraction)}: {ex.Message}");
                return BadRequest($"Could not update. Error {ex.Message}");
            }
        }

        [HttpPost()]
        [ActionName("CreateAttraction")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<AttractionDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateAttraction([FromBody] AttractionCuDto item)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateAttraction)}:");
                var resp = await _service.CreateAttractionAsync(item);
                _logger.LogInformation($"item {resp.Item.AttractionId} created");
                return Ok(resp);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{nameof(CreateAttraction)}: {ex.Message}");
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }


        public AttractionController(IAttractionService service, ILogger<AttractionController> logger)
        {
            _service = service;
            _logger = logger;
        }
    }
}
