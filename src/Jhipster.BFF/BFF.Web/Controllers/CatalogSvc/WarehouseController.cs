using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.WarehouseCm;
using Newtonsoft.Json;
using BFF.Web.DTOs.CatalogSvc;
using Microsoft.AspNetCore.Authorization;
using BFF.Web.Constants;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WarehouseController> _logger;

        public WarehouseController(IMediator mediator, ILogger<WarehouseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("AddProductWarehouse")]
        public async Task<ActionResult<int>> AddProductWarehouse([FromBody] List<WarehouseProductAddRequest> request)
        {
            _logger.LogInformation($"REST request add WarehouseProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = 0;
                foreach( var item in request)
                {
                    var tem = new WarehouseProductAddCommand
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        Lot = item.Lot,
                        DateExpire = item.DateExpire,
                        AvailabelQuantity = item.AvailabelQuantity
                    };
                    result = await _mediator.Send(tem);
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add WarehouseProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("UpdateProductWarehouse")]
        public async Task<ActionResult<int>> UpdateProductWarehouse([FromBody] List<WarehouseProductUpdateRequest> request)
        {
            _logger.LogInformation($"REST request Update WarehouseProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = 0;
                foreach (var item in request)
                {
                    var tem = new WarehouseProductAddCommand
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        DateExpire = item.DateExpire,
                        AvailabelQuantity = item.AvailabelQuantity
                    };
                    result = await _mediator.Send(tem);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to Update WarehouseProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("DeleteProductWarehouse")]
        public async Task<ActionResult<int>> UpdateProductWarehouse([FromBody] List<Guid> ids)
        {
            _logger.LogInformation($"REST request delete WarehouseProduct : {JsonConvert.SerializeObject(ids)}");
            try
            {
                var result = 0;
                foreach (var item in ids)
                {
                    var tem = new WarehouseProductDeleteCommand
                    {
                        Id = item,
                    };
                    result = await _mediator.Send(tem);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete WarehouseProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


