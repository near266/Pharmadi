using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using AutoMapper.Execution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Module.Catalog.Shared.DTOs;
using Jhipster.Domain;
using AutoMapper.QueryableExtensions;
using Microsoft.VisualBasic;
using System.Linq;
using Jhipster.Infrastructure.Migrations;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string GenerateNextProductCode(string currentSKUCode)
        {
            if (currentSKUCode.StartsWith("P"))
            {
                string currentOrderNumberString = currentSKUCode.Substring(1);
                if (int.TryParse(currentOrderNumberString, out int currentOrderNumber))
                {
                    int nextOrderNumber = currentOrderNumber + 1;
                    string nextOrderNumberString = nextOrderNumber.ToString().PadLeft(5, '0');
                    string nextOrderCode = $"P{nextOrderNumberString}";
                    return nextOrderCode;
                }
            }

            return null;
        }
        public async Task<int> Add(Product request)
        {
            //string currentSKUCode;
            //var checkSKU = await _context.Products.Select(i => i.SKU).ToListAsync();
            //if (checkSKU == null || checkSKU.Count() == 0) { currentSKUCode = "P00000"; }
            //else
            //{
            //    var Number = new List<int>();
            //    foreach (var item in checkSKU)
            //    {
            //        var s = int.Parse(item.Substring(1));
            //        Number.Add(s);
            //    };
            //    var maxNumber = Number.Max();
            //    currentSKUCode = $"P{maxNumber}";
            //}

            //request.SKU = GenerateNextProductCode(currentSKUCode);

            // gán để khi view all sắp xếp theo thời gian update mới nhất
            request.LastModifiedDate = request.CreatedDate;

            await _context.Products.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Products.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Products.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<PagedList<Product>> GetAllAdmin(int page, int pageSize, string? SKU, string? ProductName, int? status)
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.Where(i => i.Archived == false).Include(i => i.Brand)
                .Include(i => i.CategoryProducts).ThenInclude(a => a.Category).AsQueryable();
            if (SKU != null)
            {
                SKU = SKU.ToLower();
                query1 = query1.Where(i => i.SKU.ToLower().Contains(SKU) || i.ProductName.ToLower().Contains(SKU));
            }
            if (ProductName != null)
            {
                ProductName = ProductName.ToLower();
                query1 = query1.Where(i => i.SKU.ToLower().Contains(ProductName) || i.ProductName.ToLower().Contains(ProductName));
            }
            if (status != null)
            {
                query1 = query1.Where(i => i.Status == status);
            }
            var data = await query1
                        .OrderByDescending(i => i.SKU)
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<int> Update(Product request)
        {
            var old = await _context.Products.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Product, Product>(request, old);

                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Product> ViewDetail(Guid Id)
        {
            var obj = await _context.Products
                                    .Include(p => p.Brand).Include(p => p.PostContent)
                                    .Include(p => p.LabelProducts).ThenInclude(l => l.Label)
                                    .Include(p => p.TagProducts).ThenInclude(l => l.Tag)
                                    .Include(p => p.CategoryProducts).ThenInclude(l => l.Category)
                                    .Include(p => p.WarehouseProducts)
                                    .FirstOrDefaultAsync(i => i.Id == Id);
            return obj;
        }

        // int 
        public async Task<PagedList<ProductSearchDTO>> ViewProductForU(string? keyword, int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<ProductSearchDTO>();
            var query = _context.Products.Where(i => i.Archived == false).AsQueryable();
            if (keyword != null)
            {
                query = query.Where(i => i.ProductName.ToLower().Contains(keyword.ToLower()));
            }
            var query2 = await query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = i.SuggestPrice,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Archived = i.Archived,
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
                SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                BannerProduct1 = i.BannerProduct1,
                BannerProduct2 = i.BannerProduct2


            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }
        public async Task<PagedList<SaleProductDTO>> ViewProductBestSale(int page, int pageSize, Guid? userId)
        {
            //var result = new PagedList<SaleProductDTO>();
            //var query = _context.Products.Where(i => i.Archived == false).AsQueryable();

            //var query2 = await query.Select(i => new SaleProductDTO
            //{
            //    Id = i.Id,
            //    SKU = i.SKU,
            //    Price = i.Price,
            //    SalePrice = i.SalePrice,
            //    ProductName = i.ProductName,
            //    UnitName = i.UnitName,
            //    Image = i.Image,
            //    Specification = i.Specification,
            //    Archived = i.Archived,
            //    LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
            //    CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
            //    SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
            //    CanOrder = i.CanOrder,
            //    ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
            //    sellingProducts=i.sellingProducts!=null ? i.sellingProducts:0

            //}).OrderByDescending(i => i.sellingProducts).Skip(pageSize * (page - 1))
            //            .Take(pageSize)
            //            .ToListAsync();

            //result.Data = query2.AsEnumerable();
            //result.TotalCount = query.Count();
            // return result;
            var result = new PagedList<SaleProductDTO>();
            var query = await _context.Products.Where(i => i.Archived == false).AsNoTracking().ToListAsync();

            var query2 = query.Select(i => new SaleProductDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = (userId != null) ? i.SuggestPrice.ToString() : Price(i.SuggestPrice),
                SalePrice = (userId != null) ? i.SalePrice.ToString() : Price(i.SalePrice),
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                Archived = i.Archived,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ?
                    _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id)
                        .Select(i => i.Quantity)
                        .SingleOrDefault()
                        .ToString() : "0",
                SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id)
                    .Select(a => a.Quantity)
                    .SingleOrDefault(),
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                sellingProducts = i.sellingProducts != null ? i.sellingProducts : 0,
                BannerProduct1 = i.BannerProduct1,
                BannerProduct2 = i.BannerProduct2

            }).OrderByDescending(i => i.sellingProducts)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();

            result.Data = query2.ToList();
            result.TotalCount = query.Count();

            return result;
        }
        public string Price(decimal? x)
        {
            var sup = x.ToString();
            StringBuilder sb = new StringBuilder(sup);
            char replaceChar = 'x';
            for (int i = 1; i < sb.Length; i++)
            {
                sb[i] = replaceChar;
            }
            return sb.ToString();
        }
        public async Task<PagedList<NewProductDTO>> ViewProductNew(int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<NewProductDTO>();
            var query = await _context.Products.Where(i => i.Archived == false).AsQueryable().ToListAsync();

            var query2 = query.Select(i => new NewProductDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = (userId != null) ? i.SuggestPrice.ToString() : Price(i.SuggestPrice),
                SalePrice = (userId != null) ? i.SalePrice.ToString() : Price(i.SalePrice),
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                Discount = i.SuggestPrice != 0 ? (float?)(((i.SuggestPrice - i.SalePrice) / i.SuggestPrice) * 100) : 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                NewProduct = i.NewProduct != null ? i.NewProduct : 0,
                BannerProduct1 = i.BannerProduct1,
                BannerProduct2 = i.BannerProduct2

            }).OrderByDescending(i => i.NewProduct).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }
        public async Task<PagedList<ViewProductPromotionDTO>> ViewProductPromotion(string? keyword, int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<ViewProductPromotionDTO>();
            var query = _context.Products.Where(i => i.Archived == false).AsQueryable();
            if (keyword != null)
            {
                query = query.Where(i => i.ProductName.ToLower().Contains(keyword.ToLower()));
            }
            var query2 = await query.Select(i => new ViewProductPromotionDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = i.SuggestPrice,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                Archived = i.Archived,
                Discount = i.SuggestPrice != 0 ? (float?)(((i.SuggestPrice - i.SalePrice) / i.SuggestPrice) * 100) : 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                Country = i.Country,
                BannerProduct1 = i.BannerProduct1,
                BannerProduct2 = i.BannerProduct2

            }).OrderByDescending(i => i.Discount).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }
        public async Task<PagedList<ViewProductForeignDTO>> ViewProductForeign(string? keyword, int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<ViewProductForeignDTO>();
            var query = _context.Products.Where(i => i.Archived == false && i.Country.ToLower() != "việt nam").AsQueryable();
            if (keyword != null)
            {
                query = query.Where(i => i.ProductName.ToLower().Contains(keyword.ToLower()));
            }
            var listPro = await query.ToListAsync();
            var query2 = listPro.Select(p => new ViewProductForeignDTO
            {
                Id = p.Id,
                SKU = p.SKU,
                SuggestPrice = (userId != null) ? p.SuggestPrice.ToString() : Price(p.SuggestPrice),
                SalePrice = (userId != null) ? p.SalePrice.ToString() : Price(p.SalePrice),
                ProductName = p.ProductName,
                UnitName = p.UnitName,
                Image = p.Image,
                Specification = p.Specification,
                SaleNumber = _context.ProductSales.Where(s => s.ProductId == p.Id).Select(s => s.Quantity).FirstOrDefault(),
                Archived = p.Archived,
                Discount = p.SuggestPrice != 0 ? (float?)(((p.SuggestPrice - p.SalePrice) / p.SuggestPrice) * 100) : 0,
                LabelProducts = _context.LabelProducts.Include(lp => lp.Label).Where(lp => lp.ProductId == p.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(c => c.UserId == userId && c.ProductId == p.Id).Select(c => c.Quantity).FirstOrDefault().ToString() : "0",
                CanOrder = p.CanOrder,
                ShortName = p.ShortName != null ? p.ShortName : p.ProductName.Substring(0, 25),
                Country = p.Country != null ? p.Country : " ",
                ImportedProducts = p.ImportedProducts != null ? p.ImportedProducts : 0,
                BannerProduct1 = p.BannerProduct1,
                BannerProduct2 = p.BannerProduct2
            }).Where(a => a.Country.ToLower() != "việt nam").OrderByDescending(i => i.ImportedProducts).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }
        public async Task<PagedList<SearchMcProductDTO>> SearchProduct(string? keyword, List<Guid> categoryIds, List<Guid> cateLevel2Ids, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<SearchMcProductDTO>();
            var query = _context.Products.Include(i => i.Brand).Where(i => i.Archived == false).AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.SKU.ToLower().Contains(keyword) || i.ProductName.ToLower().Contains(keyword));
            }


            if (categoryIds != null && categoryIds.Count() > 0)
            {

                //check cate2 có không nếu không có thì chỉ tìm kiếm theo cate1 
                if (cateLevel2Ids != null && cateLevel2Ids.Count() > 0)
                {

                    query = query.Include(i => i.CategoryProducts).Where(i => i.CategoryProducts.Any(cp => cateLevel2Ids.Contains(cp.CategoryId)));

                }
                else
                {
                    query = query.Include(i => i.CategoryProducts).Where(i => i.CategoryProducts.Any(cp => categoryIds.Contains(cp.CategoryId)));
                    var cateId = new List<Guid>();
                    foreach (var item in categoryIds)
                    {
                        var childs = await _context.Categories.Where(i => i.ParentId == item).Select(i => i.Id).ToListAsync();
                        if (childs != null || childs.Count != 0)
                        {
                            cateId.AddRange(childs);
                        }
                        var query1 = query.Include(i => i.CategoryProducts).Where(i => i.CategoryProducts.Any(cp => cateId.Contains(cp.CategoryId)));
                        query = query.Where(i => !query1.Contains(i));
                    }



                }

            }


            if (brandIds != null && brandIds.Count() > 0)
            {
                query = query.Where(i => brandIds.Contains(i.BrandId));
            }
            if (tagIds != null && tagIds.Count() > 0)
            {
                query = query.Include(i => i.TagProducts).Where(i => i.TagProducts.Any(i => tagIds.Contains(i.TagId)));
            }
            var viewdata = await query.ToListAsync();
            var query2 = viewdata.Select(i => new SearchMcProductDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = (userId != null) ? i.SuggestPrice.ToString() : Price(i.SuggestPrice),
                SalePrice = (userId != null) ? i.SalePrice.ToString() : Price(i.SalePrice),
                ProductName = i.ProductName,
                Brand = i.Brand,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                LabelProducts = _context.LabelProducts.Include(a => a.Label).Where(a => a.ProductId == i.Id).AsEnumerable(),
                Archived = i.Archived,
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25)

            }).Skip(pageSize * (page - 1))
                        .Take(pageSize).ToList();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<int> UpdataStatusProduct(Guid id, int status)
        {
            var check = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (check != null)
            {
                check.Status = status;

                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductSearchDTO>> ViewListProductWithBrand(Guid Id, Guid? userId)
        {
            var query = _context.Products.Where(i => i.Archived == false).AsQueryable();
            var obj = query.FirstOrDefault(c => c.Id == Id);
            if (obj != null)
            {
                query = query.Where(i => (obj.Brand == null || i.Brand.BrandName.ToLower().Contains(obj.Brand.BrandName.ToLower()) && i.Id != obj.Id));
            }

            var query2 = query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = i.SuggestPrice,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Archived = i.Archived,
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                BannerProduct1 = i.BannerProduct1,
                BannerProduct2 = i.BannerProduct2

            }).Take(10).AsEnumerable();

            return query2;
        }


        public async Task<PagedList<ProductSearchDTO>> ViewListProductSimilarCategory(Guid Id, Guid? userId)
        {
            var listProduct = new PagedList<ProductSearchDTO>();
            var listCatIds = await _context.CategoryProducts.Where(i => i.ProductId == Id).Select(i => i.CategoryId).ToListAsync();
            var parentId = await _context.Categories.Where(i => listCatIds.Contains(i.Id) && i.ParentId != null).Select(i => i.ParentId).ToListAsync();
            var listId2 = await _context.Categories.Where(i => listCatIds.Contains(i.Id) && parentId.Contains(i.ParentId)).Select(i => i.Id).ToListAsync();
            var listId1 = await _context.Categories.Where(i => listCatIds.Contains(i.Id) && i.ParentId == null).Select(i => i.Id).ToListAsync();
            //var listProd = await _context.Products.ToListAsync();
            if (listId2 != null)
            {
                var prodIds = await _context.CategoryProducts.Where(i => listId2.Contains(i.CategoryId) && i.ProductId == Id).Select(i => i.ProductId).ToListAsync();
                var listProd = _context.Products.Where(i => prodIds.Contains(i.Id)).Select(i => new ProductSearchDTO
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    SuggestPrice = i.SuggestPrice,
                    SalePrice = i.SalePrice,
                    ProductName = i.ProductName,
                    UnitName = i.UnitName,
                    Image = i.Image,
                    Specification = i.Specification,
                    SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                    Archived = i.Archived,
                    LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                    CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",

                    CanOrder = i.CanOrder,
                    ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                    BannerProduct1 = i.BannerProduct1,
                    BannerProduct2 = i.BannerProduct2

                }).AsEnumerable();

                listProduct.Data = listProd.Take(10).ToList();
                listProduct.TotalCount = listProd.Count();
                return listProduct;
            }
            else
            {
                var prodId = await _context.CategoryProducts.Where(i => listId1.Contains(i.CategoryId) && i.ProductId == Id).Select(i => i.ProductId).ToListAsync();
                var ListProd = _context.Products.Where(i => prodId.Contains(i.Id)).Select(i => new ProductSearchDTO
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    SuggestPrice = i.SuggestPrice,
                    SalePrice = i.SalePrice,
                    ProductName = i.ProductName,
                    UnitName = i.UnitName,
                    Image = i.Image,
                    Specification = i.Specification,
                    SaleNumber = _context.ProductSales.Where(a => a.ProductId == i.Id).Select(a => a.Quantity).FirstOrDefault(),
                    Archived = i.Archived,
                    LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                    CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0",
                    CanOrder = i.CanOrder,
                    BannerProduct1 = i.BannerProduct1,
                    BannerProduct2 = i.BannerProduct2

                }).AsEnumerable();

                listProduct.Data = ListProd.Take(10).ToList();
                listProduct.TotalCount = ListProd.Count();
                return listProduct;
            }

            return new PagedList<ProductSearchDTO>();
        }
        public async Task<List<List<string>>> FakeData()
        {
            return await _context.Products.Select(i => i.Image).ToListAsync();
        }

        public async Task<IEnumerable<ProductSearchDTO>> SearchToChoose(string? keyword)
        {
            var query = _context.Products.AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.ProductName.ToLower().Contains(keyword));
            }
            //if (keyword == null || keyword.Length == 0 || keyword == "")
            //{
            //    query = query.Where(i => i.ProductName == "@#$");
            //}

            var query2 = query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                SuggestPrice = i.SuggestPrice,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Brand = i.Brand,
                Archived = i.Archived,
                CanOrder = i.CanOrder,
                ShortName = i.ShortName != null ? i.ShortName : i.ProductName.Substring(0, 25),
                BannerProduct1 = i.BannerProduct1,
                BannerProduct2 = i.BannerProduct2

            }).AsEnumerable();

            return query2;
        }

        public async Task<PagedList<SearchProductBrandId>> GetListProductSimilarCategoryByBrandId(int page, int pageSize, Guid brandId, Guid? userId)
        {
            var res = new PagedList<SearchProductBrandId>();
            var Pro = await _context.Products.Where(i => i.BrandId == brandId).Select(i => i.Id).ToListAsync();
            var cate = _context.CategoryProducts.Where(i => Pro.Contains(i.ProductId) && i.Priority == true)
                .Select(i => i.Category).Distinct().Where(a => a.ParentId == null);
            //var CatePro = await _context.CategoryProducts.Where(i => Pro.Contains(i.ProductId) && i.Priority == true)
            //    .Select(i => i.CategoryId).ToListAsync();
            var CateProId = cate.Select(i => i.Id).ToList();
            var CatePro = new List<Guid>();
            CatePro.AddRange(CateProId);
            foreach (var item in CateProId)
            {
                var checkcate = await _context.Categories.Where(i => i.ParentId == item).ToListAsync();
                CatePro.AddRange(checkcate.Select(i => i.Id));
            }
            var result = cate.Select(i => new SearchProductBrandId
            {
                Id = i.Id,
                CategoryName = i.CategoryName,
                Descripton = i.Descripton,
                Products = _context.CategoryProducts.Where(q => q.CategoryId == i.Id && Pro.Contains(q.ProductId)).Select(i => i.Product).ToList(),
            }).OrderBy(i => i.CategoryName)
                .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();
            res.Data = result.AsEnumerable();
            res.TotalCount = result.Count;
            return res;

        }

        public async Task<int> ArchivedProduct(List<Guid> ids)
        {
            var res = 0;
            foreach (var id in ids)
            {
                var obj = await _context.Products.FirstOrDefaultAsync(i => i.Id.Equals(id));
                if (obj != null)
                {
                    obj.Archived = true;
                    res = await _context.SaveChangesAsync();
                }
            }

            return res;
        }

        public async Task<List<Guid>> GetPorductIdbyBrandId(Guid Brandid)
        {
            var result = await _context.Products.Where(i => i.BrandId == Brandid).Select(i => i.Id).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ProductClassificationByCountryDTO>> ProductClassificationByCountry(int page, int pageSize, int Type)
        {

            var query = _context.Products.AsQueryable();
            if (Type == 1)
            {
                var listpro = query.Where(i => i.ImportedProducts >= 1 && i.Archived == false).Distinct()
                    .Select(i => new ProductClassificationByCountryDTO
                    {
                        IdBrand = i.BrandId,
                        BrandName = i.Brand.BrandName,
                        Products = _context.Products.Where(a => a.BrandId == i.BrandId).ToList(),

                    }).Skip(pageSize * (page - 1))
                        .Take(pageSize).AsEnumerable();
                return listpro;
            }
            if (Type == 2)
            {
                var listpro = query.Where(i => i.ImportedProducts == 0 && i.Archived == false).Distinct()
                    .Select(i => new ProductClassificationByCountryDTO
                    {
                        IdBrand = i.BrandId,
                        BrandName = i.Brand.BrandName,
                        Products = _context.Products.Where(a => a.BrandId == i.BrandId).ToList(),

                    }).Skip(pageSize * (page - 1))
                        .Take(pageSize).AsEnumerable();
                return listpro;

            }
            return new List<ProductClassificationByCountryDTO>();

        }
        public async Task<int> AddProductDiscount(ProductDiscount rq)
        {
            await _context.productDiscounts.AddAsync(rq);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<ProductDiscount>> ViewDiscountByUserId(Guid id)
        {
            return await _context.productDiscounts.Where(i => i.ProductId == id)
                         .OrderBy(i => i.Range).ToListAsync();
        }
        public async Task<int> DeleteDiscountProduct(Guid id)
        {
            var check = await _context.productDiscounts.FirstOrDefaultAsync(i => i.Id == id);
            _context.productDiscounts.Remove(check);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateProductDiscount(ProductDiscount rq)
        {
            var check = await _context.productDiscounts.FirstOrDefaultAsync(i => i.Id == rq.Id);
            check = _mapper.Map<ProductDiscount, ProductDiscount>(rq, check);

            return await _context.SaveChangesAsync(default);

        }
    }
}
