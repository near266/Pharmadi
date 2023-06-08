using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.ProductDisountCm;
using Module.Catalog.Application.Queries.ProductDiscountQ;
using Newtonsoft.Json;

namespace BFF.Web.Controllers.CatalogSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class ProductDiscountController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductDiscountController> _logger;
        public ProductDiscountController(IMediator mediator, IMapper mapper, ILogger<ProductDiscountController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductDiscount([FromBody] AddProductDiscountCommand rq)
        {
            _logger.LogInformation($"REST request AddProductDiscount : {JsonConvert.SerializeObject(rq)}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to AddProductDiscount fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDiscount([FromBody] UpdateProductDiscountCommand rq)
        {
            _logger.LogInformation($"REST request UpdateProductDiscount : {JsonConvert.SerializeObject(rq)}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to UpdateProductDiscount fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductDiscount([FromBody] DeleteProductDiscountCommand rq)
        {
            _logger.LogInformation($"REST request DeleteProductDiscount : {JsonConvert.SerializeObject(rq)}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to DeleteProductDiscount fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ViewDetail")]
        public async Task<IActionResult> ViewDetailProductDiscount([FromQuery] ViewDiscountByUserIdQuery rq )
        {
            _logger.LogInformation($"REST request ViewDetailProductDiscount : {JsonConvert.SerializeObject(rq)}");
            try
            {
                var result = await _mediator.Send(rq);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewDetailProductDiscount fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
