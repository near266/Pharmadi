using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public WarehouseRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Warehouse request)
        {
            await _context.Warehouses.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Warehouses.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Warehouses.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<PagedList<Warehouse>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Warehouse>();
            var query1 = _context.Warehouses.AsQueryable();
            var data = await query1
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }


        public async Task<int> Update(Warehouse request)
        {
            var old = await _context.Warehouses.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Warehouse, Warehouse>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
