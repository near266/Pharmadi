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

        public async Task<int> Delete(Guid productId, Guid tagId )
        {
            var obj = await _context.TagProducts.FirstOrDefaultAsync(i => i.ProductId.Equals(productId)&& i.TagId==tagId);
            if (obj != null)
            {
                _context.TagProducts.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(TagProduct request)
        {
            var old = await _context.TagProducts.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<TagProduct, TagProduct>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
