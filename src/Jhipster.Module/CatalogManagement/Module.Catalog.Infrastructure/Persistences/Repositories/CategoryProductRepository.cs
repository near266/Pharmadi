using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public CategoryProductRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(CategoryProduct request)

        {
            var check = await _context.CategoryProducts.FirstOrDefaultAsync(i=>i.ProductId==request.ProductId&& i.CategoryId==request.CategoryId);
            if (check == null)
            {
                await _context.CategoryProducts.AddAsync(request);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid productId, Guid categoryid)
        {
            var obj = await _context.CategoryProducts.FirstOrDefaultAsync(i => i.ProductId.Equals(productId) && i.CategoryId == categoryid);
            if (obj != null)
            {
                _context.CategoryProducts.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(CategoryProduct request)
        {
            var old = await _context.CategoryProducts.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<CategoryProduct, CategoryProduct>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
