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
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _service;
        private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelService service, ILogger<LabelController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<LabelBaseResponse>> Add([FromBody] LabelAddRequest request)
        {
            _logger.LogInformation($"REST request add Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] LabelUpdateRequest request)
        {
            _logger.LogInformation($"REST request update Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] LabelDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<LabelSearchResponse>>> Search([FromBody] LabelSearchRequest request)
        {
            _logger.LogInformation($"REST request search Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Search(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedListC<LabelSearchResponse>>> GetAllAdmin([FromBody] LabelGetAllAdminRequest request)
        {
            _logger.LogInformation($"REST request get all Label by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Label by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


