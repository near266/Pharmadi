using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.ProductCm;
using Module.Catalog.Application.Queries.ProductQ;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Newtonsoft.Json;
using BFF.Web.DTOs.CatalogSvc;
using AutoMapper;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] ProductAddCommand request)
        {
            _logger.LogInformation($"REST request add Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                request.Status = 1;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateCommand request)
        {
            _logger.LogInformation($"REST request update Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] ProductDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ViewDetail")]
        public async Task<ActionResult<Product>> ViewDetail([FromQuery] ProductViewDetailQuery request)
        {
            _logger.LogInformation($"REST request view detail Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to view detail Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<PagedList<Product>>> Search([FromBody] SearchProductQuery request)
        {
            _logger.LogInformation($"REST request search Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedList<Product>>> GetAllAdmin([FromBody] ProductGetAllAdminQuery request)
        {
            _logger.LogInformation($"REST request get all Product by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Product by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductForU")]
        public async Task<IActionResult> ViewProductForU([FromBody] ViewProductForUQuery request)
        {
            _logger.LogInformation($"REST request ViewProductForU  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductForU fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductBestSale")]
        public async Task<IActionResult> ViewProductBestSale([FromBody] ViewProductBestSaleQuery request)
        {
            _logger.LogInformation($"REST request ViewProductBestSale  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductBestSale fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductNew")]
        public async Task<IActionResult> ViewProductNew([FromBody] ViewProductNewQuery request)
        {
            _logger.LogInformation($"REST request ViewProductNew  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductNew fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ViewProductPromotion")]
        public async Task<IActionResult> ViewProductPromotion([FromBody] ViewProductPromotionQuery request)
        {
            _logger.LogInformation($"REST request ViewProductPromotion  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
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


