using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public TagRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Tag request)
        {
            await _context.Tags.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Tags.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Tags.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(Tag request)
        {
            var old = await _context.Tags.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Tag, Tag>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
        public async Task<IEnumerable<Tag>> Search(string? keyword)
        {
            keyword = keyword.ToLower();
            var query = await _context.Tags.Where(i=>i.TagName.ToLower().Contains(keyword))
                        .ToListAsync();
            return query;
        }

        public async Task<PagedList<Tag>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Tag>();
            var query1 = _context.Tags.AsQueryable();
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
