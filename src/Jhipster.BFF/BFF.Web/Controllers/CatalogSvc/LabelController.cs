using BFF.Web.Constants;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.LabelCm;
using Module.Catalog.Application.Queries.LabelQ;
using Module.Catalog.Domain.Entities;


using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [Authorize]
    [ApiController]
    [Route("gw/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LabelController> _logger;

        public LabelController(IMediator mediator, ILogger<LabelController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] LabelAddCommand request)
        {
            _logger.LogInformation($"REST request add Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var UserId = Guid.Parse(GetUserIdFromContext());
                request.CreatedBy=UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] LabelUpdateCommand request)
        {
            _logger.LogInformation($"REST request update Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var UserId = Guid.Parse(GetUserIdFromContext());
                request.LastModifiedBy = UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] LabelDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.MERCHANT)]

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<Label>>> Search([FromBody] LabelSearchQuery request)
        {
            _logger.LogInformation($"REST request search Label : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Label fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedList<Label>>> GetAllAdmin([FromBody] LabelGetAllAdminQuery request)
        {
            _logger.LogInformation($"REST request get all Label by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Label by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}


