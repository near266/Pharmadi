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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService service, ILogger<BrandController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<BrandBaseResponse>> Add([FromBody] BrandAddRequest request)
        {
            _logger.LogInformation($"REST request add Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] BrandUpdateRequest request)
        {
            _logger.LogInformation($"REST request update Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] BrandDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<BrandSearchResponse>>> Search([FromBody] BrandSearchRequest request)
        {
            _logger.LogInformation($"REST request search Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Search(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedListC<BrandSearchResponse>>> GetAllAdmin([FromBody] BrandGetAllAdminRequest request)
        {
            _logger.LogInformation($"REST request get all Brand by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Brand by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


