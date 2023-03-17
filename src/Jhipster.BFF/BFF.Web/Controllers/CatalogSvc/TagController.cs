using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagService service, ILogger<TagController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<TagBaseResponse>> Add([FromBody] TagAddRequest request)
        {
            _logger.LogInformation($"REST request add Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] TagUpdateRequest request)
        {
            _logger.LogInformation($"REST request update Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] TagDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<TagSearchResponse>>> Search([FromBody] TagSearchRequest request)
        {
            _logger.LogInformation($"REST request search Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Search(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedListC<TagSearchResponse>>> GetAllAdmin([FromBody] TagGetAllAdminRequest request)
        {
            _logger.LogInformation($"REST request get all Tag by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Tag by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


