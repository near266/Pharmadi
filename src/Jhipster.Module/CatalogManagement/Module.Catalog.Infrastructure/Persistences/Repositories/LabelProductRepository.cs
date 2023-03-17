using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class LabelProductRepository : ILabelProductRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public LabelProductRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(LabelProduct request)
        {
            await _context.LabelProducts.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.LabelProducts.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.LabelProducts.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(LabelProduct request)
        {
            var old = await _context.LabelProducts.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<LabelProduct, LabelProduct>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
