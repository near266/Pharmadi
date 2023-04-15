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

        public async Task<int> Delete(Guid productId)
        {
            var obj = await _context.LabelProducts.Where(i => i.ProductId.Equals(productId)).ToListAsync();
            if (obj != null)
            {
                foreach (var item in obj)
                {
                    _context.LabelProducts.Remove(item);
                    await _context.SaveChangesAsync();
                }
                return 1;

            }
            return 0;
        }

    }
}
