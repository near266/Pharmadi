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
    public class GroupBrandController : ControllerBase
    {
        private readonly IGroupBrandService _service;
        private readonly ILogger<GroupBrandController> _logger;

        public GroupBrandController(IGroupBrandService service, ILogger<GroupBrandController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<GroupBrandBaseResponse>> Add([FromBody] GroupBrandAddRequest request)
        {
            _logger.LogInformation($"REST request add GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] GroupBrandUpdateRequest request)
        {
            _logger.LogInformation($"REST request update GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] GroupBrandDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<GroupBrandSearchResponse>>> Search([FromBody] GroupBrandSearchRequest request)
        {
            _logger.LogInformation($"REST request search GroupBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Search(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search GroupBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedListC<GroupBrandSearchResponse>>> GetAllAdmin([FromBody] GroupBrandGetAllAdminRequest request)
        {
            _logger.LogInformation($"REST request get all GroupBrand by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all GroupBrand by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


