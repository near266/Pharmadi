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
using BFF.Web.DTOs.OrderSvc;
using AutoMapper;
using Module.Ordering.Application.Commands.OrderItemCm;
using Module.Ordering.Application.Commands.CartCm;
using Module.Ordering.Application.Queries.CartQ;
using Module.Factor.Application.Queries.MerchantQ;
using Module.Ordering.Application.Commands.HistoryOrderCm;

namespace BFF.Web.ProductSvc
{
    //[ApiController]
    [Route("gw/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(IMediator mediator, IMapper mapper, ILogger<PurchaseOrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        [HttpPost("AddByUser")]
        public async Task<ActionResult<int>> Add([FromBody] OrderAddRequestUser request)
        {
            _logger.LogInformation($"REST request add PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedBy = new Guid(GetUserIdFromContext());
                request.Status = 1;
                request.MerchantId = new Guid(GetUserIdFromContext());
                request.CreatedDate = DateTime.Now;


                //lay thong tin merchant
                var cusRequest = new MerchantViewDetailQuery
                {
                    Id = request.MerchantId
                };
                var cus = await _mediator.Send(cusRequest);

                request.MerchantName = cus.MerchantName;
                request.PhoneNumber = cus.PhoneNumber;
                request.ContactName = cus.ContactName;
                request.ContractNumber = cus.ContractNumber;
                request.Address = cus.Address;


                if (request.TotalPrice <= 1000000) request.ShippingFee = 50000;

                var res1 = 0;
                // add order
                var step1 = _mapper.Map<PurchaseOrderAddCommand>(request);
                res1 = await _mediator.Send(step1);

                // get cart choice
                var step2 = new CartGetAllChoiceQuery
                {
                    userId = request.MerchantId
                };
                var temp2 = await _mediator.Send(step2);

                //add order item
                foreach (var c in temp2)
                {
                    var res2 = new OrderItemAddCommand
                    {
                        Id = Guid.NewGuid(),
                        PurchaseOrderId = request.Id,
                        ProductId = (Guid)c.ProductId,
                        Quantity = (int)c.Quantity
                    };
                    res1 = await _mediator.Send(res2);
                }

                //remove cart
                var step3 = new CartDeleteCommand
                {
                    ids = temp2.Select(i => i.Id).ToList()
                };
                res1 = await _mediator.Send(step3);

                return Ok(res1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddByAdmin")]
        public async Task<ActionResult<int>> AddByAdmin([FromBody] OrderAddRequestAdmin request)
        {
            _logger.LogInformation($"REST request add PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedBy = new Guid(GetUserIdFromContext());
                request.Status = 2;
                request.CreatedDate = DateTime.Now;

                // add order
                var step1 = _mapper.Map<PurchaseOrderAddCommand>(request);
                await _mediator.Send(step1);


                //add order item
                foreach (var c in request.orderItemRequests)
                {
                    var res2 = new OrderItemAddCommand
                    {
                        Id = Guid.NewGuid(),
                        PurchaseOrderId = request.Id,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity
                    };
                    await _mediator.Send(res2);
                }


                return Ok(1);
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
                request.LastModifiedBy = new Guid(GetUserIdFromContext());
                var mapPurchaseOrder = _mapper.Map<PurchaseOrderUpdateCommand>(request);
                var value = await _mediator.Send(mapPurchaseOrder);
                int result = 0;
                if (request.OrderItem.orderItemAdds.Count() > 0)
                {
                    foreach (var item in request.OrderItem.orderItemAdds)
                    {
                        var tem = new OrderItemAddCommand
                        {
                            Id = Guid.NewGuid(),
                            PurchaseOrderId = mapPurchaseOrder.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        };
                        result = await _mediator.Send(tem);
                    }
                }
                if (request.OrderItem.orderItemUpdates.Count() > 0)
                {
                    foreach (var item in request.OrderItem.orderItemUpdates)
                    {
                        var tem = new OrderItemUpdateCommand
                        {
                            Id = item.Id,
                            PurchaseOrderId = mapPurchaseOrder.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        };
                        result = await _mediator.Send(tem);
                    }
                }
                if (request.OrderItem.orderItemDelete.Count() > 0)
                {
                    var tem = new OrderItemDeleteCommand
                    {
                        Ids = request.OrderItem.orderItemDelete
                    };
                    result = await _mediator.Send(tem);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] List<PurchaseOrderDeleteCommand> request)
        {
            _logger.LogInformation($"REST request delete PurchaseOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                var value = 0;
                foreach (var item in request)
                {
                    var result = await _mediator.Send(item);
                    var history = new HistoryOrderCommand()
                    {
                        Id = item.Id,
                    };
                    await _mediator.Send(history);
                    value += result;
                }
                return Ok(value);
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
                if (request.userId == null || request.userId == Guid.Empty)
                    request.userId = new Guid(GetUserIdFromContext());
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
                //if (request.Status == 2)
                //{
                //    // lay ds item trong don hang can phe duyet
                //    var step1 = new OrderItemGetAllByOrderQuery
                //    {
                //        purchaseOrderId = request.Id,
                //        page = 1,
                //        pageSize = 1000
                //    };
                //    var temp1 = await _mediator.Send(step1);

                //    // tien hanh check so luong trong kho
                //    foreach (var item in temp1.Data)
                //    {
                //        var step2 = new CountProductQuery
                //        {
                //            id = item.ProductId,
                //        };
                //        var temp2 = await _mediator.Send(step2);
                //        if (temp2 < item.Quantity) throw new ArgumentException("There are not enough products in Warehouse", nameof(item.Product.ProductName));
                //    }
                //    // khi tat ca so luong du tien hanh cap nhat lai kho
                //    foreach (var item in temp1.Data)
                //    {
                //        var step3 = new ListLotDateByProductQuery
                //        {
                //            id = item.ProductId,
                //        };
                //        var temp3 = await _mediator.Send(step3);
                //        //for(int i=0;i<temp3.Count(); i++)
                //        //{
                //        //    var quantity = item.Quantity;
                //        //    while(quantity > item2.AvailabelQuantity)
                //        //    {
                //        //        var step4 = new WarehouseProductDeleteCommand
                //        //        {
                //        //            Id = item2.Id,
                //        //        };
                //        //        await _mediator.Send(step4);
                //        //        quantity -= item2.AvailabelQuantity;
                //        //    }
                //        //    //if(quantity)
                //        //}
                //    }

                //}

                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update PurchaseOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Merchant/transactionHistory")]
        public async Task<IActionResult> transactionHistory([FromBody] HistoryPurchaseOrderQuery rq)
        {

            try
            {
                var request = GetUserIdFromContext();

                rq.id = Guid.Parse(request);
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}


