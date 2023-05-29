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
using Microsoft.AspNetCore.Authorization;
using BFF.Web.Constants;
using Module.Catalog.Application.Commands.CategoryCm;
using Module.Catalog.Application.Commands.WarehouseCm;
using Module.Catalog.Application.Commands.TagCm;
using Module.Catalog.Application.Commands.LabelCm;
using Microsoft.Extensions.Caching.Distributed;
using Module.Catalog.Shared.DTOs;
using Module.Redis.Configurations;
using Module.Redis.Library.Helpers;
using Google.Apis.Logging;
using System.Linq;

namespace BFF.Web.ProductSvc
{
    [ApiController]
    [Route("gw/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly RedisConfig _redisConfiguration;

        private readonly IDistributedCache _cache;
        public ProductController(IMediator mediator, RedisConfig redisConfig, IDistributedCache cache, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _cache = cache;
            _redisConfiguration = redisConfig;
        }
        private string GetUserIdFromContext()
        {
            return User.FindFirst("UserId")?.Value;
        }
        [Authorize(Roles = RolesConstants.ADMIN)]


        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] ProductAddRequest request)
        {
            _logger.LogInformation($"REST request add Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.Id = Guid.NewGuid();
                request.CreatedDate = DateTime.Now;
                var UserId = Guid.Parse(GetUserIdFromContext());
                request.CreatedBy = UserId;
                int result = 0;

                //add product
                //var step1 = _mapper.Map<ProductAddCommand>(request);
                var step1 = new ProductAddCommand
                {
                    Id = request.Id,
                    SKU = request.SKU,
                    ProductName = request.ProductName,
                    UserObject = request.Function,
                    PostContentId = request.PostContentId,
                    SalePrice = request.SalePrice,
                    SuggestPrice = request.SuggestPrice,
                    Description = request.Description,
                    UnitName = request.UnitName,
                    BrandId = request.BrandId,
                    Status = request.Status,
                    Image = request.Image,
                    Industry = request.Industry,
                    Warning = request.Effect,
                    Preserve = request.Preserve,
                    Dosage = request.Dosage,
                    DosageForms = request.DosageForms,
                    Country = request.Country,
                    Ingredient = request.Ingredient,
                    Usage = request.Usage,
                    Specification = request.Specification,
                    Number = request.Number,
                    CreatedBy = request.CreatedBy,
                    CreatedDate = request.CreatedDate,
                    Archived = false,
                    HideProduct = request.HideProduct,
                    CanOrder = request.CanOrder,
                    NewProduct = request.NewProduct,
                    ImportedProducts = request.ImportedProducts,
                    sellingProducts = request.sellingProducts,
                    ShortName=request.ShortName,
                    BannerProduct1= request.BannerProduct1,
                    BannerProduct2= request.BannerProduct2,
                };
                await _mediator.Send(step1);

                //add categoryProduct
                if (request.categoryProductAdds != null)
                {
                    foreach (var item in request.categoryProductAdds)
                    {
                        var step3 = new CategoryProductAddCommand
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
                            CategoryId = item.CategoryId,
                            Priority = item.Priority,
                        };
                        await _mediator.Send(step3);
                    }
                }


                //add warehouse product
                if (request.warehouseProductAdds != null)
                {
                    foreach (var item in request.warehouseProductAdds)
                    {
                        var step4 = new WarehouseProductAddCommand
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
                            Lot = item.Lot,
                            DateExpire = item.DateExpire,
                            AvailabelQuantity = item.AvailabelQuantity

                        };
                        await _mediator.Send(step4);
                    }

                }

                //add tag product
                if (request.TagIds != null)
                {
                    foreach (var item in request.TagIds)
                    {
                        var step5 = new TagProductAddCommand
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
                            TagId = item

                        };
                        await _mediator.Send(step5);
                    }
                }


                //add tag product
                if (request.LabelIds != null)
                {
                    foreach (var item in request.LabelIds)
                    {
                        var step6 = new LabelProductAddCommand
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
                            LabelId = item

                        };
                        await _mediator.Send(step6);
                    }
                }

                return Ok(1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to add Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = RolesConstants.ADMIN)]



        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            _logger.LogInformation($"REST request update Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                int result = 0;
                request.LastModifiedDate = DateTime.Now;
                request.LastModifiedBy = Guid.Parse(GetUserIdFromContext());
                var step1 = new ProductUpdateCommand
                {
                    Id = request.Id,
                    SKU = request.SKU,
                    ProductName = request.ProductName,
                    UserObject = request.UserObject,
                    PostContentId = request.PostContentId,
                    SalePrice = request.SalePrice,
                    SuggestPrice = request.SuggestPrice,
                    Description = request.Description,
                    UnitName = request.UnitName,
                    BrandId = request.BrandId,
                    Status = request.Status,
                    Image = request.Image,
                    Industry = request.Industry,
                    Warning = request.Warning,
                    Preserve = request.Preserve,
                    Dosage = request.Dosage,
                    DosageForms = request.DosageForms,
                    Country = request.Country,
                    Ingredient = request.Ingredient,
                    Usage = request.Usage,
                    Specification = request.Specification,
                    Number = request.Number,
                    LastModifiedBy = request.LastModifiedBy,
                    LastModifiedDate = request.LastModifiedDate,
                    Archived = request.Archived,
                    HideProduct = request.HideProduct,
                    CanOrder = request.CanOrder,
                    NewProduct = request.NewProduct,
                    ImportedProducts = request.ImportedProducts,
                    sellingProducts = request.sellingProducts,
                    ShortName=request.ShortName,
                    BannerProduct1=request.BannerProduct1,
                    BannerProduct2=request.BannerProduct2,
                };

                result = await _mediator.Send(step1);

                //add tag product
                if (request.TagIds != null)
                {
                    var step10 = new TagProductDeleteCommand
                    {
                        productId = request.Id
                    };
                    await _mediator.Send(step10);

                    foreach (var item in request.TagIds)
                    {
                        var step5 = new TagProductAddCommand
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
                            TagId = item

                        };
                        await _mediator.Send(step5);
                    }
                }


                //add tag product
                if (request.LabelIds != null)
                {
                    var step10 = new LabelProductDeleteCommand
                    {
                        productId = request.Id
                    };
                    await _mediator.Send(step10);
                    foreach (var item in request.LabelIds)
                    {
                        var step6 = new LabelProductAddCommand
                        {
                            Id = Guid.NewGuid(),
                            ProductId = request.Id,
                            LabelId = item

                        };
                        await _mediator.Send(step6);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to update Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = RolesConstants.ADMIN)]

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
                try
                {
                    request.userId = Guid.Parse(GetUserIdFromContext());
                }
                catch (Exception ex)
                {
                    _logger.LogError($"REST request to search Product fail: {ex.Message}");
                    return StatusCode(401, ex.Message);
                }
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
                //var res = _mapper.Map<PagedList<GetAllAminDTO>>(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to get all Product by Admin  fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("ViewProductForU")]
        public async Task<IActionResult> ViewProductForU([FromBody] ViewProductForUQuery request)
        {
            _logger.LogInformation($"REST request ViewProductForU  : {JsonConvert.SerializeObject(request)}");
            try
            {
                try
                {
                    request.userId = Guid.Parse(GetUserIdFromContext());
                }
                catch
                { }
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductForU fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [AllowAnonymous]

        [HttpPost("ViewProductBestSale")]
        public async Task<IActionResult> ViewProductBestSale([FromBody] ViewProductBestSaleQuery request)
        {
            _logger.LogInformation($"REST request ViewProductBestSale  : {JsonConvert.SerializeObject(request)}");
            try
            {
                //PagedList<SaleProductDTO>? res;

                //string recordKey = $"{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
                //res = await _cache.GetRecordAsync<PagedList<SaleProductDTO>>(recordKey);
                //if (res is null)
                //{
                  
                //    res = await _mediator.Send(request);
                //    await _cache.SetRecordAsync(recordKey, res, TimeSpan.FromSeconds(3));
                //}
                var res = await _mediator.Send(request);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductBestSale fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [AllowAnonymous]

        [HttpPost("ViewProductNew")]
        public async Task<IActionResult> ViewProductNew([FromBody] ViewProductNewQuery request)
        {
            _logger.LogInformation($"REST request ViewProductNew  : {JsonConvert.SerializeObject(request)}");
            try
            {
                try
                {
                    request.userId = Guid.Parse(GetUserIdFromContext());
                }
                catch (Exception ex)
                {

                }
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductNew fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [AllowAnonymous]

        [HttpPost("ViewProductPromotion")]
        public async Task<IActionResult> ViewProductPromotion([FromBody] ViewProductPromotionQuery request)
        {
            _logger.LogInformation($"REST request ViewProductPromotion  : {JsonConvert.SerializeObject(request)}");
            try
            {
                try
                {
                    request.userId = Guid.Parse(GetUserIdFromContext());
                }
                catch
                {

                }
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductPromotion fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [AllowAnonymous]

        [HttpPost("ViewProductForeign")]
        public async Task<IActionResult> ViewProductForeign([FromBody] ViewProductForeignQuery request)
        {
            _logger.LogInformation($"REST request ViewProductForeign  : {JsonConvert.SerializeObject(request)}");
            try
            {
                try
                {
                    request.userId = Guid.Parse(GetUserIdFromContext());
                }
                catch
                {

                }
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductForeign fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("UpdateStatusProduct")]
        public async Task<IActionResult> UpdateStatusProduct([FromBody] UpdateStatusProductCommand request)
        {
            _logger.LogInformation($"REST request UpdateStatusProduct  : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to UpdateStatusProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("ViewProductWithBrand")]
        public async Task<IActionResult> ViewProductWithBrand([FromBody] ViewProductWithBrandQuery request)
        {
            _logger.LogInformation($"REST request ViewProductWithBrand : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.userId = Guid.Parse(GetUserIdFromContext());
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductWithBrand fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("ViewProductSimilar")]
        public async Task<IActionResult> ViewProductSimilarCategory([FromBody] ViewProductSimilarQuery request)
        {
            _logger.LogInformation($"REST request ViewProductSimilarCategory : {JsonConvert.SerializeObject(request)}");
            try
            {
                request.userId = Guid.Parse(GetUserIdFromContext());
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ViewProductSimilarCategory fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("ImageProduct")]
        public async Task<IActionResult> ImageProduct([FromBody] FakeDataQuery request)
        {
            _logger.LogInformation($"REST request ImageProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ImageProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SearchToChoose")]
        public async Task<IActionResult> SearchToChoose([FromBody] ProductSearchToChooseQuery request)
        {
            _logger.LogInformation($"REST request ProductSearchToChoo : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ProductSearchToChoose fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("GetListProductSimilarCategoryByBrandId")]
        public async Task<IActionResult> GetListProductSimilarCategoryByBrandId([FromBody] GetListProductSimilarCategoryByBrandIdQuery request)
        {
            _logger.LogInformation($"REST request GetListProductSimilarCategoryByBrandId : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to GetListProductSimilarCategoryByBrandId fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("PinProductInBrand")]
        public async Task<ActionResult<int>> PinProductInBrand([FromBody] PinProductRequest request)
        {
            _logger.LogInformation($"REST request Pin Product : {JsonConvert.SerializeObject(request)}");
            try
            {
                var pro = new GetProductIdByBrandIdQuery { 
                
                 BrandId=request.brandId
                };
                

                var temp = await _mediator.Send(pro);
                
          
                foreach (var p in request.Products)
                    {
                    if (temp.Any(i=>i==p.ProductId))
                    {
                    var update = new UpdateStatusProductCommand { Id = p.ProductId, Status = p.Status};
                     await _mediator.Send(update);
                    }
                    }
                return Ok(1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to Pin Product fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("ShortName")]
        public async Task<ActionResult<string>> GetShortName([FromQuery] ProductViewDetailQuery request)
        {
            _logger.LogInformation($"REST request ShortName : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result.ShortName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ShortName fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Archived")]
        public async Task<IActionResult> ArchivedProduct([FromBody] ProductArchivedCommand request)
        {
            _logger.LogInformation($"REST request ArchivedProduct : {JsonConvert.SerializeObject(request)}");
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REST request to ArchivedProduct fail: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
       
    }
}


