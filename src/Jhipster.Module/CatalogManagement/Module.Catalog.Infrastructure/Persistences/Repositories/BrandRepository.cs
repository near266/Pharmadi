using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public BrandRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Brand request)
        {
            await _context.Brands.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Brands.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Brands.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(Brand request)
        {
            var old = await _context.Brands.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Brand, Brand>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }

        public async Task<IEnumerable<Brand>> Search(string? keyword)
        {
            keyword = keyword.ToLower();
            var query = await _context.Brands.Where(i => i.BrandName.ToLower().Contains(keyword))
                        .ToListAsync();
            return query;
        }

        public async Task<PagedList<Brand>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Brand>();
            var query1 = _context.Brands.AsQueryable();
            var data = await query1
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

      
    }
}
