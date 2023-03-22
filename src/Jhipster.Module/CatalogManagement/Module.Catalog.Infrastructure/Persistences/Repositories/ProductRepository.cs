using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using AutoMapper.Execution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<int> Add(Product request)
        {
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

        public async Task<PagedList<Product>> GetAllAdmin(int page, int pageSize, string? SKU,string? ProductName,int? status)
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.AsQueryable();
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
                query1 = query1.Where(i=> i.Status ==status);
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
                                    .Include(p=>p.CategoryProducts).ThenInclude(l => l.Category)
                                    .Include(p=>p.WarehouseProducts)
                                    .FirstOrDefaultAsync(i => i.Id == Id);
            return obj;
        }

        // int 
        public async Task<PagedList<Product>> ViewProductForU(string? keyword, int page, int pageSize)
        {
            var query1= _context.Products.AsQueryable();
            var result = new PagedList<Product>();
            var query = await _context.Products.Where(i => i.ProductName.ToLower().Contains(keyword.ToLower()))
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = query;
            result.TotalCount = query1.Count();
            return result;
        }
        public async Task<PagedList<Product>> ViewProductBestSale(int page, int pageSize)
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.AsQueryable();

            var query = await _context.Products
                .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = query;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<PagedList<Product>> ViewProductNew(int page, int pageSize)
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.AsQueryable();

            var query = await _context.Products.Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = query;
            result.TotalCount = query1.Count();
            return result;
        }
        public async Task<PagedList<Product>> ViewProductPromotion(string? keyword, int page, int pageSize)
        {
            var query1 = _context.Products.AsQueryable();

            var result = new PagedList<Product>();
            var query = await _context.Products.Where(i => i.ProductName.ToLower().Contains(keyword.ToLower()) && i.SalePrice !=0)
                .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = query;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<PagedList<Product>> SearchProduct(string? keyword, List<Guid?>? categoryIds, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize)
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query1 = query1.Where(i => i.SKU.ToLower().Contains(keyword) || i.ProductName.ToLower().Contains(keyword));
            }
            if (categoryIds.Count() > 0)
            {
                query1 = query1.Include(i => i.CategoryProducts).Where(i => i.CategoryProducts.Any(i => categoryIds.Contains(i.CategoryId)));
            }
            if (brandIds.Count() > 0)
            {
                query1 = query1.Where(i => brandIds.Contains(i.BrandId));
            }
            if (tagIds.Count() > 0)
            {
                query1 = query1.Include(i => i.TagProducts).Where(i => i.TagProducts.Any(i => tagIds.Contains(i.TagId)));
            }
            var data = await query1
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<int> UpdataStatusProduct(Guid id, int status)
        {
            var check = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (check != null)
            {
                check.Status = status;
                return 1;
            }
            return 0;
        }

        public async Task<IEnumerable<Product>> ViewListProductWithBrand(Guid Id)
        {
            var query = _context.Products.AsQueryable();
            var obj = query.FirstOrDefault(c => c.Id == Id);
            if (obj != null)
            {

                query = query.Where(i => i.Brand.BrandName.ToLower().Contains(obj.Brand.BrandName.ToLower()) && i.Id != obj.Id);
            }
            var result = query.AsEnumerable();
            return result;
        }

      

        public async Task<IEnumerable<Product>> ViewListProductSimilarCategory(Guid Id)
        {
            var listCatIds = await _context.CategoryProducts.Where(i => i.ProductId == Id).Select(i=>i.CategoryId).ToListAsync();
            var listId2 = await _context.Categories.Where(i =>listCatIds.Contains(i.Id) && i.ParentId!= null).Select(i=>i.Id).ToListAsync();
            var listId1 = await _context.Categories.Where(i => listCatIds.Contains(i.Id) && i.ParentId == null).Select(i=>i.Id).ToListAsync();
            //var listProd = await _context.Products.ToListAsync();
            if(listId2!= null)
            {
                var prodIds = await _context.CategoryProducts.Where(i => listId2.Contains(i.CategoryId)).Select(i=>i.ProductId).ToListAsync();
                var listProd = _context.Products.Where(i=>prodIds.Contains(i.Id)).AsEnumerable();
                return listProd;
            }
            else
            {
                var prodId = await _context.CategoryProducts.Where(i => listId1.Contains(i.CategoryId)).Select(i => i.ProductId).ToListAsync();
                var ListProd = _context.Products.Where(i => prodId.Contains(i.Id)).AsEnumerable();
                return ListProd;
            }
            var res = new List<Product>();
            return res;
        }
        public async Task<List<List<string>>> FakeData()
        {
            return await _context.Products.Select(i => i.Image).ToListAsync();
        }
    }
}
