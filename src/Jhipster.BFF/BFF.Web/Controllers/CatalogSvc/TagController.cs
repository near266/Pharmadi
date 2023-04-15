using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.TagCm;
using Module.Catalog.Application.Queries.TagQ;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BFF.Web.Constants;
using BFF.Web.DTOs.CatalogSvc;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TagController> _logger;

        public TagController(IMediator mediator, ILogger<TagController> logger)
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
        public async Task<ActionResult<int>> Add([FromBody] TagAddCommand request)
        {
            _logger.LogInformation($"REST request add Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var UserId = Guid.Parse(GetUserIdFromContext());
                request.CreatedBy = UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] TagUpdateCommand request)
        {
            _logger.LogInformation($"REST request update Tag : {JsonConvert.SerializeObject(request)}");
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
                _logger.LogError($"REST request to update Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] TagDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<Tag>>> Search([FromBody] TagSearchQuery request)
        {
            _logger.LogInformation($"REST request search Tag : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Tag fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedList<Tag>>> GetAllAdmin([FromBody] TagGetAllAdminQuery request)
        {
            _logger.LogInformation($"REST request get all Tag by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Tag by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("AddProductTag")]
        public async Task<ActionResult<int>> AddProductTag([FromBody] List<TagProductAddRequest> request)
        {
            _logger.LogInformation($"REST request add TagProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = 0;
                foreach (var item in request)
                {
                    var tem = new TagProductAddCommand
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        TagId = item.TagId
                    };
                    result = await _mediator.Send(tem);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add TagProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

       
    }
}


