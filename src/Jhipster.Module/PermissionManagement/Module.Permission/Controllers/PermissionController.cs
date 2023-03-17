// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Jhipster.Crosscutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Permission.Application.Commands;
using Module.Permission.Application.Dtos;
using Module.Permission.Application.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(IMediator mediator, ILogger<PermissionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Tìm kiếm Function
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpGet("function/search")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<FunctionAllDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<FunctionAllDTO>>> SearchFunction([FromQuery] FunctionSearchQuery request)
        {
            _logger.LogDebug($"REST request to search function : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Tìm kiếm Function theo Id
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpGet("function/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(FunctionDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FunctionDTO>> GetFunctionById(Guid id)
        {
            _logger.LogDebug($"REST request to get function by id : {id}");
            try
            {
                var request = new FunctionByIdQuery { Id = id };
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Tạo Function
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("function/create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateFunction([FromBody] FunctionAddCommand request)
        {
            _logger.LogDebug($"REST request to create function : {JsonConvert.SerializeObject(request)} by {User.FindFirst("sub")?.Value}");
            try
            {
                request.CreatedBy = User.FindFirst("sub")?.Value;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật Function
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("function/update")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(FunctionDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FunctionDTO>> CreateFunction([FromBody] FunctionUpdateCommand request)
        {
            _logger.LogDebug($"REST request to update function : {JsonConvert.SerializeObject(request)} by {User.FindFirst("sub")?.Value}");
            try
            {
                request.LastModifiedBy = User.FindFirst("sub")?.Value;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Xóa Function
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("function/delete")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> DeleteFunction(FunctionDeleteCommand request)
        {
            _logger.LogDebug($"REST request to delete function : {JsonConvert.SerializeObject(request)} by {User.FindFirst("sub")?.Value}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Tìm kiếm Function Type
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpGet("function-type/search")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<FunctionTypeDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<FunctionTypeDTO>>> SearchFunctionType([FromQuery] FunctionTypeSearchQuery request)
        {
            _logger.LogDebug($"REST request to search function type : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Tìm kiếm Function Type theo Id
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpGet("function-type/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(FunctionTypeDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FunctionTypeDTO>> GetFunctionTypeById(Guid id)
        {
            _logger.LogDebug($"REST request to get function type by id : {id}");
            try
            {
                var request = new FunctionTypeByIdQuery { Id = id };
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Tạo Function Type
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("function-type/create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateFunctionType([FromBody] FunctionTypeAddCommand request)
        {
            _logger.LogDebug($"REST request to create function type : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật Function Type
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("function-type/update")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(FunctionTypeDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<FunctionTypeDTO>> UpdateFunctionType([FromBody] FunctionTypeUpdateCommand request)
        {
            _logger.LogDebug($"REST request to update function type : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Xóa Function Type
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("function-type/delete")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> DeleteFunctionType(FunctionTypeDeleteCommand request)
        {
            _logger.LogDebug($"REST request to delete function type : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
