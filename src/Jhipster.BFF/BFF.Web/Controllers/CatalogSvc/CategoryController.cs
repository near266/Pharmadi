using BFF.Web.Constants;
using Jhipster.Service.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.CategoryCm;
using Module.Catalog.Application.Queries.CategoryQ;
using Module.Catalog.Domain.Entities;



using Newtonsoft.Json;

namespace BFF.Web.ProductSvc
{
    [Authorize]
    [ApiController]
    [Route("gw/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
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
        public async Task<ActionResult<int>> Add([FromBody] CategoryAddCommand request)
        {
            _logger.LogInformation($"REST request add Category : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var UserId = new Guid(GetUserIdFromContext());
                request.CreatedBy = UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Category fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateCommand request)
        {
            _logger.LogInformation($"REST request update Category : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.LastModifiedDate = DateTime.Now;
                var UserId = new Guid(GetUserIdFromContext());
                request.LastModifiedBy = UserId;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Category fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] CategoryDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete Category : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Category fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.MERCHANT)]

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<Category>>> Search([FromBody] CategorySearchQuery request)
        {
            _logger.LogInformation($"REST request search Category : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Category fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedList<Category>>> GetAllAdmin([FromBody] CategoryGetAllAdminQuery request)
        {
            _logger.LogInformation($"REST request get all Category by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Category by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.MERCHANT)]

        [HttpPost("GetListCategory")]
        public async Task<ActionResult<PagedList<Category>>> GetListCatelory([FromBody] GetListCategotyQuery request)
        {
            _logger.LogInformation($"REST request get list Category : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get list Category by fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}






