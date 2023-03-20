using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

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

        public async Task<PagedList<Product>> GetAllAdmin(int page, int pageSize, string? keyword )
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.AsQueryable();
            if(keyword!= null)
            {
                keyword = keyword.ToLower();
                query1 = query1.Where(i => i.SKU.ToLower().Contains(keyword) || i.ProductName.ToLower().Contains(keyword));
            }
            var data = await query1
                        .Skip(pageSize * page)
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
                                    .Include(p=>p.Brand).Include(p=>p.PostContent)
                                    .Include(p=>p.LabelProducts).ThenInclude(l=>l.Label)
                                    .Include(p => p.TagProducts).ThenInclude(l => l.Tag)
                                    .FirstOrDefaultAsync(i => i.Id == Id);
            return obj;
        }

        // int 
        public async Task<IEnumerable<Product>> ViewProductForU(int page, int pageSize)
        {
            var query= await _context.Products.Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            return query;
        }
        public async Task<IEnumerable<Product>> ViewProductBestSale(int page, int pageSize)
        {
            var query = await _context.Products.Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            return query;
        }

        public async Task<IEnumerable<Product>> ViewProductNew(int page, int pageSize)
        {
            var query = await _context.Products.Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            return query;
        }
        public async Task<IEnumerable<Product>> ViewProductPromotion(int page, int pageSize)
        {
            var query = await _context.Products.Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            return query;
        }

        public async Task<PagedList<Product>> SearchProduct(string? keyword,List<Guid?>? categoryIds, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize)
        {
            var result = new PagedList<Product>();
            var query1 = _context.Products.AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query1 = query1.Where(i => i.SKU.ToLower().Contains(keyword) || i.ProductName.ToLower().Contains(keyword));
            }
            if (categoryIds.Count()>0)
            {
                query1 = query1.Include(i=>i.CategoryProducts).Where(i => i.CategoryProducts.Any(i => categoryIds.Contains(i.CategoryId)));
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
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<IEnumerable<Product>> ViewProductList(Guid Id)
        {
          
            var query= _context.Products.AsQueryable();
            var data= await query.AsNoTracking().IgnoreAutoIncludes().Where(c=>c.Id==Id).ToListAsync();
          
            return data;
        }
    }
}
