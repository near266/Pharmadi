using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class PostContentRepository : IPostContentRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public PostContentRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(PostContent request)
        {
            await _context.PostContents.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.PostContents.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.PostContents.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<PagedList<PostContent>> GetAll(int page, int pageSize)
        {
            var result = new PagedList<PostContent>();
            var query1 = _context.PostContents.AsQueryable();
            var data = await query1
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<PostContent> ViewDetail(Guid id)
        {
            var result = await _context.PostContents.FirstOrDefaultAsync(i=>i.Id.Equals(id));
            return result;
        }

        public async Task<int> Update(PostContent request)
        {
            var old = await _context.PostContents.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<PostContent, PostContent>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
