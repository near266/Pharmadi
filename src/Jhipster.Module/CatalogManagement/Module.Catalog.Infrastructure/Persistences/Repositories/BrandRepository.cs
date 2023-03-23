using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

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
            var query = _context.Brands.AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.BrandName.ToLower().Contains(keyword));
            }
            var result = query.AsEnumerable();
            return result;
        }

        public async Task<PagedList<Brand>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Brand>();
            var query1 = _context.Brands.AsQueryable();
            var data = await query1
                        .Skip(pageSize * (page-1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<int> PinBrand(Guid Id,bool? Pin)
        {
            var check = await _context.Brands.FirstOrDefaultAsync(i=>i.Id == Id);
            if (check != null)
            {
                check.Pin = Pin;
                return 1;
            }
            return 0;
        }

        public async Task<PagedList<Brand>> IsHaveGroup(int page, int pageSize, int type,Guid? GroupBrandId)
        {
               var query = _context.Brands.AsQueryable();
                var result = new PagedList<Brand>();
            if(type == 1)
            {
                var data = await query.Where(i=>i.GroupBrandId !=null).Include(i=>i.GroupBrand) .Skip(pageSize * (page - 1))
                        .Take(pageSize).ToListAsync();
                result.Data = data;
                result.TotalCount = query.Count();
                return result;
            }
            if(type == 2)
            {

                var data = await query.Where(i => i.GroupBrandId == null).Include(i => i.GroupBrand).Skip(pageSize * (page - 1))
                        .Take(pageSize).ToListAsync();
                result.Data = data;
                result.TotalCount = query.Count();
                return result;
            }
            return result;

        }
    }
}
