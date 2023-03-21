using BFF.Web.Constants;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Queries.PurchaseOrderQ;
using Module.Ordering.Application.Commands.PurchaseOrderCm;
using Module.Ordering.Domain.Entities;
using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(IMediator mediator, ILogger<PurchaseOrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] PurchaseOrderAddCommand request)
        {
            _logger.LogInformation($"REST request add PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] PurchaseOrderUpdateCommand request)
        {
            _logger.LogInformation($"REST request update PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] PurchaseOrderDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllPurchaseOrderByAdmin")]
        public async Task<ActionResult<PagedList<PurchaseOrder>>> GetAllPurchaseOrderByAdmin([FromBody] PurchaseOrderGetAllByAdminQuery request)
        {
            _logger.LogInformation($"REST request GetAllPurchaseOrderByAdmin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllPurchaseOrderByAdmin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("GetAllPurchaseOrderByUser")]
        public async Task<IActionResult> GetAllPurchaseOrderByUser([FromBody] PurchaseOrderGetAllByUserQuery request)
        {
            _logger.LogInformation($"REST request GetAllPurchaseOrderByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllPurchaseOrderByUser  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("ViewDetailPurchaseOrder")]
        public async Task<IActionResult> ViewDetailPurchaseOrder(PurchaseOrderViewDetailQuery request)
        {
            _logger.LogInformation($"REST request ViewDetailPurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
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


