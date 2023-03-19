using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Persistences;
using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService service, ILogger<CartController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<CartBaseResponse>> Add([FromBody] CartAddRequest request)
        {
            _logger.LogInformation($"REST request add Cart : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Cart fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CartUpdateRequest request)
        {
            _logger.LogInformation($"REST request update Cart : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Cart fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] CartDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete Cart : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Cart fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllCartByUser")]
        public async Task<ActionResult<PagedListC<CartInfor>>> GetAllCartByUser(CartGetAllByUserRequest request)
        {
            _logger.LogInformation($"REST request GetAllCartByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllCartByUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllCartByUser  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


