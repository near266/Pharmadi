// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Permission.Application.Commands;
using Module.Permission.Application.Dtos;
using Module.Permission.Application.Queries;
using Newtonsoft.Json;
using System.Net;

namespace Module.Permission.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IMediator mediator, ILogger<RoleController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Tìm kiếm nhóm quyền
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize(Roles = RolesConstants.ADMIN)]
        [HttpGet("search")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<RoleDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> SearchRole([FromQuery] RoleSearchQuery request)
        {
            _logger.LogDebug($"REST request to search role : {JsonConvert.SerializeObject(request)}");
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
        /// Tạo nhóm quyền
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateRole([FromBody] RoleRqDTO request)
        {
            _logger.LogDebug($"REST request to create role : {JsonConvert.SerializeObject(request)}");
            try
            {
                var username = User.FindFirst("sub")?.Value;
                var createRoleRq = new RoleAddCommand { Name = request.Name };
                var resultCreateRoleRq = await _mediator.Send(createRoleRq);

                if(resultCreateRoleRq == 1)
                {
                    var createRoleFunctionRq = new RoleFunctionAddCommand {
                        functions = request.functions,
                        roleId = request.Name.ToLowerInvariant(),
                        CreatedBy = username
                    };

                    var res = await _mediator.Send(createRoleFunctionRq);
                    return Ok(res);
                }

                throw new Exception("Create role fail");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật nhóm quyền
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize(Roles = RolesConstants.ADMIN)]
        [HttpPut("update")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UpdateRole([FromBody] RoleUpdateRqDTO request)
        {
            _logger.LogDebug($"REST reques to update role : {JsonConvert.SerializeObject(request)}");
            try
            {
                var username = User.FindFirst("sub")?.Value;
                var updateRoleRq = new RoleUpdateCommand
                {
                    Id = request.Id,
                    Name = request.Name
                };
                var resUpdateRole = await _mediator.Send(updateRoleRq);
                if(resUpdateRole != null)
                {
                    var updateRoleFunctionRq = new RoleFunctionUpdateCommand {
                        roleId = request.Id,
                        functions = request.functions,
                        ModifiedBy = username
                    };
                    var res = await _mediator.Send(updateRoleFunctionRq);
                    return Ok(res);
                }

                throw new Exception("Update role fail");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Xóa nhóm quyền
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = RolesConstants.ADMIN)]
        [HttpDelete("delete")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> DeleteRole(RoleDeleteCommand request)
        {
            _logger.LogDebug($"REST request to delete role : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lấy nhóm quyền theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Roles = RolesConstants.ADMIN)]
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(RoleByIdRsDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoleByIdRsDTO>> GetRoleById(string id)
        {
            _logger.LogDebug($"REST request to get role by id : {id}");
            try
            {
                var result = new RoleByIdRsDTO();

                var roleByIdRq = new RoleByIdQuery { Id = id };
                var roleByIdRs = await _mediator.Send(roleByIdRq);
                if(roleByIdRs == null)
                {
                    throw new Exception("Role not found");
                }
                result.Role = roleByIdRs;

                var roleFuntionRq = new RoleFunctionSearchQuery {
                    roleId = id,
                    status = true,
                    page = 0,
                    pageSize = 1000
                };
                var roleFuntionRs = await _mediator.Send(roleFuntionRq);
                result.RoleFunctions = roleFuntionRs;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
