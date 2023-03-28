using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using AutoMapper.Execution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Module.Catalog.Shared.DTOs;
using Jhipster.Domain;

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
                // Tách số từ chuỗi mã hiện tại
                string currentOrderNumberString = currentSKUCode.Substring(1);
                if (int.TryParse(currentOrderNumberString, out int currentOrderNumber))
                {
                    // Tăng giá trị số đó lên một đơn vị
                    int nextOrderNumber = currentOrderNumber + 1;
                    // Định dạng số thành chuỗi theo đúng định dạng
                    string nextOrderNumberString = nextOrderNumber.ToString().PadLeft(5, '0');
                    // Ghép lại thành chuỗi mã mới
                    string nextOrderCode = $"P{nextOrderNumberString}";
                    return nextOrderCode;
                }
            }

            // Trả về null nếu chuỗi mã hiện tại không đúng định dạng
            return null;
        }
        public async Task<int> Add(Product request)
        {
            string currentSKUCode;
            var checkSKU = await _context.Products.Select(i => i.SKU).ToListAsync();
            if (checkSKU == null) { currentSKUCode = "S00000"; }
            else
            {
                var Number = new List<int>();
                foreach (var item in checkSKU)
                {
                    var s = int.Parse(item.Substring(1));
                    Number.Add(s);
                };
                var maxNumber = Number.Max();
                currentSKUCode = $"P{maxNumber}";
            }

            request.SKU = GenerateNextProductCode(currentSKUCode);

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
            var query1 = _context.Products.Where(i => i.Archived == false).AsQueryable();
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

                return await _context.SaveChangesAsync(default);
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
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Archived = i.Archived,
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }
        public async Task<PagedList<ProductSearchDTO>> ViewProductBestSale(int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<ProductSearchDTO>();
            var query = _context.Products.Where(i => i.Archived == false).AsQueryable();

            var query2 = await query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                Archived = i.Archived,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<PagedList<ProductSearchDTO>> ViewProductNew(int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<ProductSearchDTO>();
            var query = _context.Products.Where(i => i.Archived == false).AsQueryable();

            var query2 = await query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }
        public async Task<PagedList<ProductSearchDTO>> ViewProductPromotion(string? keyword, int page, int pageSize, Guid? userId)
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
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                Archived = i.Archived,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

            result.Data = query2.AsEnumerable();
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<PagedList<ProductSearchDTO>> SearchProduct(string? keyword, List<Guid?>? categoryIds, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize, Guid? userId)
        {
            var result = new PagedList<ProductSearchDTO>();
            var query = _context.Products.Include(i => i.Brand).Where(i => i.Archived == false).AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.SKU.ToLower().Contains(keyword) || i.ProductName.ToLower().Contains(keyword));
            }
            if (categoryIds != null && categoryIds.Count() > 0)
            {
                query = query.Include(i => i.CategoryProducts).Where(i => i.CategoryProducts.Any(i => categoryIds.Contains(i.CategoryId)));
            }
            if (brandIds != null && brandIds.Count() > 0)
            {
                query = query.Where(i => brandIds.Contains(i.BrandId));
            }
            if (tagIds != null && tagIds.Count() > 0)
            {
                query = query.Include(i => i.TagProducts).Where(i => i.TagProducts.Any(i => tagIds.Contains(i.TagId)));
            }

            var query2 = await query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                Brand = i.Brand,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Archived = i.Archived,
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();

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
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Archived = i.Archived,
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).AsEnumerable();

            return query2;
        }


        public async Task<PagedList<ProductSearchDTO>> ViewListProductSimilarCategory(Guid Id, int page, int pageSize, Guid? userId)
        {
            var listProduct = new PagedList<ProductSearchDTO>();
            var listCatIds = await _context.CategoryProducts.Where(i => i.ProductId == Id).Select(i => i.CategoryId).ToListAsync();
            var listId2 = await _context.Categories.Where(i => listCatIds.Contains(i.Id) && i.ParentId != null).Select(i => i.Id).ToListAsync();
            var listId1 = await _context.Categories.Where(i => listCatIds.Contains(i.Id) && i.ParentId == null).Select(i => i.Id).ToListAsync();
            //var listProd = await _context.Products.ToListAsync();
            if (listId2 != null)
            {
                var prodIds = await _context.CategoryProducts.Where(i => listId2.Contains(i.CategoryId)).Select(i => i.ProductId).ToListAsync();
                var listProd = _context.Products.Where(i => prodIds.Contains(i.Id)).Select(i => new ProductSearchDTO
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    Price = i.Price,
                    SalePrice = i.SalePrice,
                    ProductName = i.ProductName,
                    UnitName = i.UnitName,
                    Image = i.Image,
                    Specification = i.Specification,
                    SaleNumber = 0,
                    Archived = i.Archived,
                    LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                    CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

                }).AsEnumerable();

                listProduct.Data = listProd;
                listProduct.TotalCount = listProd.Count();
                return listProduct;
            }
            else
            {
                var prodId = await _context.CategoryProducts.Where(i => listId1.Contains(i.CategoryId)).Select(i => i.ProductId).ToListAsync();
                var ListProd = _context.Products.Where(i => prodId.Contains(i.Id)).Select(i => new ProductSearchDTO
                {
                    Id = i.Id,
                    SKU = i.SKU,
                    Price = i.Price,
                    SalePrice = i.SalePrice,
                    ProductName = i.ProductName,
                    UnitName = i.UnitName,
                    Image = i.Image,
                    Specification = i.Specification,
                    SaleNumber = 0,
                    Archived = i.Archived,
                    LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                    CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

                }).AsEnumerable();

                listProduct.Data = ListProd;
                listProduct.TotalCount = ListProd.Count();
                return listProduct;
            }

            return listProduct;
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
                query = query.Where(i => i.ProductName.ToLower().Contains(keyword) || i.SKU.ToLower().Contains(keyword));
            }

            var query2 = query.Select(i => new ProductSearchDTO
            {
                Id = i.Id,
                SKU = i.SKU,
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                Brand = i.Brand,
                Archived = i.Archived

            }).AsEnumerable();

            return query2;
        }

        public async Task<IEnumerable<ProductSearchDTO>> GetListProductSimilarCategoryByBrandId(Guid brandId, Guid? userId)
        {
            var pro = _context.Products.Where(i => i.BrandId == brandId).Select(i => i.Id).ToList();
            var Cate = await _context.CategoryProducts.Where(i => pro.Contains(i.ProductId)).Select(i => i.CategoryId).ToListAsync();
            var Listpro = await _context.CategoryProducts.Where(i => Cate.Contains(i.CategoryId)).Select(i => i.ProductId).ToListAsync();

            var result = await _context.Products.Where(i => Listpro.Contains(i.Id)).Select(i => new ProductSearchDTO
            {

                Id = i.Id,
                SKU = i.SKU,
                Price = i.Price,
                SalePrice = i.SalePrice,
                ProductName = i.ProductName,
                UnitName = i.UnitName,
                Image = i.Image,
                Specification = i.Specification,
                SaleNumber = 0,
                Archived = i.Archived,
                LabelProducts = _context.LabelProducts.Include(i => i.Label).Where(i => i.ProductId == i.Id).AsEnumerable(),
                CartNumber = (userId != null) ? _context.Carts.Where(a => a.UserId == userId && a.ProductId == i.Id).Select(i => i.Quantity).FirstOrDefault().ToString() : "0"

            }).ToListAsync();
            return result;
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
    }
}
