using BFF.Web.Constants;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Ordering.Application.Queries.OrderItemQ;
using Module.Ordering.Application.Queries.PurchaseOrderQ;
using Module.Ordering.Application.Commands.PurchaseOrderCm;
using Module.Ordering.Domain.Entities;
using Newtonsoft.Json;
using Module.Catalog.Application.Queries.CategoryQ;
using Module.Catalog.Application.Queries.WarehouseProductQ;
using Module.Catalog.Application.Commands.WarehouseCm;

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

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] PurchaseOrderUpdateStatusCommand request)
        {
            _logger.LogInformation($"REST request update PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            { 
                //status ==2 xac nhan don hang
                if(request.Status==2)
                {
                    // lay ds item trong don hang can phe duyet
                    var step1 = new OrderItemGetAllByOrderQuery
                    { 
                        purchaseOrderId = request.Id,
                        page =1,
                        pageSize =1000
                    };
                    var temp1 = await _mediator.Send(step1);
                    
                    // tien hanh check so luong trong kho
                    foreach(var item in temp1.Data)
                    {
                        var step2 = new CountProductQuery
                        {
                            id = item.ProductId,
                        };
                        var temp2 = await _mediator.Send(step2);
                        if(temp2 < item.Quantity) throw new ArgumentException("There are not enough products in Warehouse", nameof(item.Product.ProductName));
                    }
                    // khi tat ca so luong du tien hanh cap nhat lai kho
                    foreach(var item in temp1.Data)
                    {
                        var step3 = new ListLotDateByProductQuery
                        {
                            id = item.ProductId,
                        };
                        var temp3 = await _mediator.Send(step3); 
                        //for(int i=0;i<temp3.Count(); i++)
                        //{
                        //    var quantity = item.Quantity;
                        //    while(quantity > item2.AvailabelQuantity)
                        //    {
                        //        var step4 = new WarehouseProductDeleteCommand
                        //        {
                        //            Id = item2.Id,
                        //        };
                        //        await _mediator.Send(step4);
                        //        quantity -= item2.AvailabelQuantity;
                        //    }
                        //    //if(quantity)
                        //}
                    }

                }

                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


