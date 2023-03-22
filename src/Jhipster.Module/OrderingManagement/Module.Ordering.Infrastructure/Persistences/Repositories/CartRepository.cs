using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;

namespace Module.Factor.Infrastructure.Persistence.Repositories
{
    public class CartRepository : ICartRepostitory
    {
        private readonly OrderingDbContext _context;
        private readonly IMapper _mapper;
        public CartRepository(OrderingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Cart request)
        {
            await _context.Carts.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(List<Guid> ids)
        {
            foreach(var id in ids)
            {
                var obj = await _context.Carts.FirstOrDefaultAsync(i => i.Id.Equals(id));
                if (obj != null)
                {
                    _context.Carts.Remove(obj);
                    await _context.SaveChangesAsync();
                }
            }
      
            return 1;
        }


        public async Task<PagedList<Cart>> GetAllByUser(int page, int pageSize, Guid userId)
        {

            var query = _context.Carts.Where(c => c.UserId == userId).Select(c => new Cart
            {
                Id = c.Id,
                UserId = c.UserId,
                Product = _context.Products.Where(i => i.Id == c.ProductId)
                                .Include(i => i.Brand)
                                .Include(i => i.TagProducts).ThenInclude(i => i.Tag)
                                .Include(i => i.LabelProducts).ThenInclude(i => i.Label)
                                .FirstOrDefault(),
                Quantity = c.Quantity
            }).AsQueryable();
            
            var data = await query
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize).ToListAsync();

            var res = new PagedList<Cart>();
            res.Data = data;
            res.TotalCount = query.Count();
            return res;
        }

        public async Task<int> Update(Cart request)
        {
            var old = await _context.Carts.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Cart, Cart>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }

        public async Task<List<Cart>> GetCartChoice(Guid userId)
        {
            var data = await _context.Carts.Where(i => i.UserId == userId && i.IsChoice == true).ToListAsync();
            return data;
        }
    }
}
