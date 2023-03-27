using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Microsoft.Extensions.Logging.Abstractions;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class GroupBrandRepository : IGroupBrandRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public GroupBrandRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(GroupBrand request)
        {
            await _context.GroupBrands.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.GroupBrands.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if(obj!= null)
            {
                _context.GroupBrands.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(GroupBrand request)
        {
            var old = await _context.GroupBrands.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<GroupBrand, GroupBrand>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }

        public async Task<PagedList<GroupBrand>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<GroupBrand>();
            var query1 = _context.GroupBrands.AsQueryable();
            var data = await query1
                        .Skip(pageSize *( page-1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<IEnumerable<GroupBrand>> Search(string? keyword)
        {
            var query = _context.GroupBrands.AsQueryable();
            if(keyword != null)
            {

            keyword = keyword.ToLower();
             query = query.Where(i => i.GroupBrandName.ToLower().Contains(keyword));
            }
            var result = query.AsEnumerable();
            return result;
        }

        public async Task<int> PinGroup(Guid Id, bool? Pin)
        {
            var check = await _context.GroupBrands.FirstOrDefaultAsync(i=>i.Id.Equals(Id));
            if (check != null)
            {
                if (check.Pin == null) { check.Pin=false; }
                check.Pin = Pin;
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        
        
    }
}
