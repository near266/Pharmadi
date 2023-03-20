using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public LabelRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Label request)
        {
            await _context.Labels.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Labels.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Labels.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<Label>> Search(string? keyword)
        {
            keyword = keyword.ToLower();
            var query = await _context.Labels.Where(i=>i.LabelName.ToLower().Contains(keyword)).ToListAsync();
            return query;
        }

        public async Task<PagedList<Label>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Label>();
            var query1 = _context.Labels.AsQueryable();
            var data = await query1
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<int> Update(Label request)
        {
            var old = await _context.Labels.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Label, Label>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
