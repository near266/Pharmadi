using BFF.Web.Constants;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Ordering.Application.Queries.CartQ;
using Module.Ordering.Application.Commands.CartCm;
using Module.Ordering.Domain.Entities;
using Newtonsoft.Json;
using Module.Catalog.Domain.Entities;

namespace BFF.Web.ProductSvc
{
    //[ApiController]
    [Route("gw/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CartController> _logger;

        public CartController(IMediator mediator, ILogger<CartController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }


        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] CartAddCommand request)
        {
            _logger.LogInformation($"REST request add Cart : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.UserId = new Guid(GetUserIdFromContext());
                request.IsChoice = false;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Cart fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CartUpdateCommand request)
        {
            _logger.LogInformation($"REST request update Cart : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.UserId = Guid.Parse(GetUserIdFromContext());
                request.IsChoice = true;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Cart fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] CartDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete Cart : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Cart fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllCartByUser")]
        public async Task<ActionResult<PagedList<IGrouping<Brand, Cart>>>> GetAllCartByUser(CartGetAllByUserQuery request)
        {
            _logger.LogInformation($"REST request GetAllCartByUser : {JsonConvert.SerializeObject(request)}");
            try
            {
                if (request.userId == null || request.userId == Guid.Empty)
                    request.userId = new Guid(GetUserIdFromContext());
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllCartByUser  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CartReuslt")]
        public async Task<IActionResult> CartReuslt([FromBody] CartResultSumQuery request)
        {
            _logger.LogInformation($"REST request CartResultSumQuery : {JsonConvert.SerializeObject(request)}");
            try
            {
                if (request.userId == null || request.userId == Guid.Empty)
                    request.userId = new Guid(GetUserIdFromContext());
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to CartResultSumQuery fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("QuickOrder")]
        public async Task<IActionResult> QuickOrder([FromBody] QuickOrderQuery request)
        {

            try
            {
                if (request.userId == null || request.userId == Guid.Empty)
                    request.userId = new Guid(GetUserIdFromContext());
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to QuickOrder fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


