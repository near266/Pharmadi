using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

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
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<IEnumerable<GroupBrand>> Search(string? keyword)
        {
            if(keyword != null)
            {

            keyword = keyword.ToLower();
            var query = await _context.GroupBrands.Where(i => i.GroupBrandName.ToLower().Contains(keyword))
                        .ToListAsync();
            return query;
            }
            return null;
        }

    }
}
