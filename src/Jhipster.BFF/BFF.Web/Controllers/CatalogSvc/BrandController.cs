using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.BrandCm;
using Module.Catalog.Application.Queries.BrandQ;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using BFF.Web.Constants;
using Module.Catalog.Shared.DTOs;
using BFF.Web.DTOs.CatalogSvc;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BrandController> _logger;
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }

        public BrandController(IMediator mediator, ILogger<BrandController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] BrandAddCommand request)
        {
            _logger.LogInformation($"REST request add Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var UserId= new Guid(GetUserIdFromContext());
                request.CreatedBy= UserId;
                request.Archived = false;
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
      
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] BrandUpdateCommand request)
        {
            _logger.LogInformation($"REST request update Brand : {JsonConvert.SerializeObject(request)}");
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
                _logger.LogError($"REST request to update Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] BrandDeleteCommand request)
        {
            _logger.LogInformation($"REST request delete Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<Brand>>> Search([FromBody] BrandSearchQuery request)
        {
            _logger.LogInformation($"REST request search Brand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to search Brand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("GetAllAdmin")]
        public async Task<ActionResult<PagedList<Brand>>> GetAllAdmin([FromBody] BrandGetAllAdminQuery request)
        {
            _logger.LogInformation($"REST request get all Brand by Admin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Brand by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("PinBrand")]
        public async Task<IActionResult> PinBrand ([FromBody] BrandPinCommand request)
        {
            _logger.LogInformation($"REST request get all Brand Pin : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Brand Pin fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    
        [HttpPost("GetListBrandIsHaveGroup")]
        public async Task<IActionResult> GetListBrandIsHaveGroup([FromBody] GetListBrandIsHaveGroupIdQuery request)
        {
            _logger.LogInformation($"REST request GetListBrandIsHaveGroup : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request GetListBrandIsHaveGroup fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("ImageBrand")]
        public async Task<IActionResult> ImageBrand([FromBody] ImageBrandQuery request)
        {
            _logger.LogInformation($"REST request ImageBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request ImageBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
       
        [HttpPost("AddBrandToGroup")]
        public async Task<ActionResult<int>> AddBrandToGroup([FromBody] AddBrandToGroupRequest request)
        {
            _logger.LogInformation($"REST request AddBrandToGroup : {JsonConvert.SerializeObject(request)}");
            try
            {
       
                foreach (var rq in request.BrandIds)
                {
                    var br = new BrandUpdateCommand { 
                      Id=rq.Id,
                      GroupBrandId=request.GroupId,
                
                      
                    };
                    var result =  await _mediator.Send(br);

                }
                return Ok(1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request AddBrandToGroup fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("BrandDetail")]
        public async Task<IActionResult> BrandRepresentative([FromBody] BrandDetailQuery request)
        {
            _logger.LogInformation($"REST request BrandDetail : {JsonConvert.SerializeObject(request)}");
            try
            {


                var result = await _mediator.Send(request);


                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request BrandDetail fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}


