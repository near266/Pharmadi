using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Persistences;
using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _service;
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(IOrderItemService service, ILogger<OrderItemController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<OrderItemBaseResponse>> Add([FromBody] OrderItemAddRequest request)
        {
            _logger.LogInformation($"REST request add OrderItem : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add OrderItem fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] OrderItemUpdateRequest request)
        {
            _logger.LogInformation($"REST request update OrderItem : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update OrderItem fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] OrderItemDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete OrderItem : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete OrderItem fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllOrderItemByUser")]
        public async Task<ActionResult<PagedListC<OrderItemInfor>>> GetAllOrderItemByUser(ItemGetAllByOrderRequest request)
        {
            _logger.LogInformation($"REST request GetAllOrderItemByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ItemsGetAllByOrder(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllOrderItemByUser  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


