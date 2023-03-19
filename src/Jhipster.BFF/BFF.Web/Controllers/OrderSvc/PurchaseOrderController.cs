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
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _service;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(IPurchaseOrderService service, ILogger<PurchaseOrderController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<PurchaseOrderBaseResponse>> Add([FromBody] PurchaseOrderAddRequest request)
        {
            _logger.LogInformation($"REST request add PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var result = await _service.Add(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] PurchaseOrderUpdateRequest request)
        {
            _logger.LogInformation($"REST request update PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _service.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] PurchaseOrderDeleteRequest request)
        {
            _logger.LogInformation($"REST request delete PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllPurchaseOrderByAdmin")]
        public async Task<ActionResult<PagedListC<PurchaseOrderInforAdmin>>> GetAllPurchaseOrderByAdmin([FromBody] PurchaseOrderGetAllAdminRequest request)
        {
            _logger.LogInformation($"REST request GetAllPurchaseOrderByAdmin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllPurchaseOrderByAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllPurchaseOrderByAdmin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("GetAllPurchaseOrderByUser")]
        public async Task<IActionResult> GetAllPurchaseOrderByUser([FromBody] PurchaseOrderGetAllUserRequest request)
        {
            _logger.LogInformation($"REST request GetAllPurchaseOrderByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.GetAllPurchaseOrderByUser(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllPurchaseOrderByUser  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ViewDetailPurchaseOrder")]
        public async Task<IActionResult> ViewDetailPurchaseOrder(PurchaseOrderViewDetailRequest request)
        {
            _logger.LogInformation($"REST request ViewDetailPurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _service.ViewDetailPurchaseOrder(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewDetailPurchaseOrder  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


