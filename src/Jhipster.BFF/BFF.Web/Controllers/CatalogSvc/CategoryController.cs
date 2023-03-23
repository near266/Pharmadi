using BFF.Web.Constants;
using BFF.Web.DTOs.CatalogSvc;
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
        private List<string> GetListUserRole()
        {
            var key = User.FindFirst("auth")?.Value;
            return key.Split(',').ToList();
        }
        private string GetUserRole()
        {
            return User.FindFirst("auth")?.Value;
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

        [HttpPost("GetTwoLayer")]
        public async Task<ActionResult<IEnumerable<Category>>> CategoryGetTwoLayer([FromBody] CategoryGetTwoLayerQuery request)
        {
            _logger.LogInformation($"REST request CategoryGetTwoLayerQuery : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to CategoryGetTwoLayerQuery fail: {ex.Message}");
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

        [HttpPost("GetListCategoryLv1")]
        public async Task<ActionResult<PagedList<Category>>> GetListCateloryLv1 ([FromBody] ViewListCategoryLv1Query request)
        {
            _logger.LogInformation($"REST request get list Category lv1: {JsonConvert.SerializeObject(request)}");
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
        [HttpPost("GetListCategoryLv2")]
        public async Task<ActionResult<PagedList<Category>>> GetListCateloryLv2([FromBody] ViewListCategoryLv2Query request)
        {
            _logger.LogInformation($"REST request get list Category lv2: {JsonConvert.SerializeObject(request)}");
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
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("AddProductCategory")]
        public async Task<ActionResult<int>> AddProductCategory([FromBody] List<CategoryProductAddRequest> request)
        {
            _logger.LogInformation($"REST request add CategoryProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = 0;
                foreach (var item in request)
                {
                    var tem = new CategoryProductAddCommand
                    {
                        Id = Guid.NewGuid(),
                        ProductId = item.ProductId,
                        CategoryId = item.CategoryId,
                        Priority = item.Priority,
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
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("UpdateProductTag")]
        public async Task<ActionResult<int>> UpdateProductTag([FromBody] List<CategoryProductUpdateRequest> request)
        {
            _logger.LogInformation($"REST request Update TagProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = 0;
                foreach (var item in request)
                {
                    var tem = new CategoryProductUpdateCommand
                    {
                        ProductId = item.ProductId,
                        CategoryId = item.CategoryId,
                        Priority = item.Priority,
                    };
                    result = await _mediator.Send(tem);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to Update TagProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

        [HttpPost("DeleteProductCategory")]
        public async Task<ActionResult<int>> UpdateProductCategory([FromBody] List<CategoryProductDeleteRequest> ids)
        {
            _logger.LogInformation($"REST request delete CategoryProduct : {JsonConvert.SerializeObject(ids)}");
            try
            {
                var result = 0;
                foreach (var item in ids)
                {
                    var tem = new CategoryProductDeleteCommand
                    {
                        Id = item.Id,
                        productId = item.ProductId
                    };
                    result = await _mediator.Send(tem);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to delete CategoryProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}






