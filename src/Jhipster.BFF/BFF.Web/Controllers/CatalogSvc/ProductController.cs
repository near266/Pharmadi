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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<ProductBaseResponse>> Add([FromBody] ProductAddRequest request)
        {
            _logger.LogInformation($"REST request add Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.Status = 1;
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            _logger.LogInformation($"REST request update Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] ProductDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ViewDetail")]
        public async Task<ActionResult<ProductInforSearchResponse>> ViewDetail([FromQuery] ProductViewDetailRequest request)
        {
            _logger.LogInformation($"REST request view detail Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ViewDetail(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to view detail Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<PagedListC<ProductInforSearchResponse>>> Search([FromBody] ProductSearchRequest request)
        {
            _logger.LogInformation($"REST request search Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Search(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedListC<ProductGetAllAdminResponse>>> GetAllAdmin([FromBody] ProductGetAllAdminRequest request)
        {
            _logger.LogInformation($"REST request get all Product by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Product by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductForU")]
        public async Task<IActionResult> ViewProductForU([FromBody] ProductSearchListRequest request)
        {
            _logger.LogInformation($"REST request ViewProductForU  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ViewProductForU(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductForU fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductBestSale")]
        public async Task<IActionResult> ViewProductBestSale([FromBody] ProductSearchListRequest request)
        {
            _logger.LogInformation($"REST request ViewProductBestSale  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ViewProductForU(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductBestSale fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductNew")]
        public async Task<IActionResult> ViewProductNew([FromBody] ProductSearchListRequest request)
        {
            _logger.LogInformation($"REST request ViewProductNew  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ViewProductNew(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductNew fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductPromotion")]
        public async Task<IActionResult> ViewProductPromotion([FromBody] ProductSearchListRequest request)
        {
            _logger.LogInformation($"REST request ViewProductPromotion  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ViewProductPromotion(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductPromotion fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


