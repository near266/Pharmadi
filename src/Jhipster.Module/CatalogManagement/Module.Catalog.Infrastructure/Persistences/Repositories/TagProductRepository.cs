using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class TagProductRepository : ITagProductRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public TagProductRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(TagProduct request)
        {
            await _context.TagProducts.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid productId)
        {
            var obj = await _context.TagProducts.Where(i => i.ProductId.Equals(productId)).ToListAsync();
            if (obj != null)
            {
                foreach(var item in obj)
                {
                    _context.TagProducts.Remove(item);
                    await _context.SaveChangesAsync();
                }
                return 1;
               
            }
            return 0;
        }

       
    }
}
