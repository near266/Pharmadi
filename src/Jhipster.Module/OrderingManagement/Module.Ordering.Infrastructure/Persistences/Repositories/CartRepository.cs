using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;

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

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Carts.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Carts.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            var query = await _context.Carts
                        //.Skip(pageSize * page)
                        //.Take(pageSize)
                        .ToListAsync();
            return query;
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
    }
}
