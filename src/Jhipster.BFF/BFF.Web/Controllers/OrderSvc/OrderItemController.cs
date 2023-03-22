﻿using BFF.Web.Constants;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Ordering.Application.Queries.OrderItemQ;
using Module.Ordering.Application.Commands.OrderItemCm;
using Module.Ordering.Domain.Entities;
using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(IMediator mediator, ILogger<OrderItemController> logger)
        {
            _mediator= mediator;
            _logger = logger;
        }
        // Lấy ra thông tin của Token
        private string GetUserId()//userId
        {
            return User.FindFirst("UserId")?.Value;
        }
        private List<string> GetListUserRole()
        {
            var key= User.FindFirst("auth")?.Value;
            return key.Split(',').ToList();
        }
        private string GetUserRole()
        {
            return User.FindFirst("auth")?.Value;
        }
       

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] OrderItemAddCommand request)
        {
            _logger.LogInformation($"REST request add OrderItem : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add OrderItem fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] OrderItemUpdateCommand request)
        {
            _logger.LogInformation($"REST request update OrderItem : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update OrderItem fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] OrderItemDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete OrderItem : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete OrderItem fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetAllOrderItemByOrder")]
        public async Task<ActionResult<PagedList<OrderItem>>> GetAllOrderItemByUser(OrderItemGetAllByOrderQuery request)
        {
            _logger.LogInformation($"REST request GetAllOrderItemByOrder : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetAllOrderItemByOrder  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


