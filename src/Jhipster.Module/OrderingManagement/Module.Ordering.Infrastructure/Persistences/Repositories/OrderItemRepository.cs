using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Module.Ordering.Shared.Utilities;

namespace Module.Factor.Infrastructure.Persistence.Repositories
{
    public class OrderItemRepository : IOrderItemRepostitory
    {
        private readonly OrderingDbContext _context;
        private readonly IMapper _mapper;
        public OrderItemRepository(OrderingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(OrderItem request)
        {
            await _context.OrderItems.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.OrderItems.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<PagedList<OrderItem>> GetAllItemByOrder(int page, int pageSize, Guid OrderId)
        {
            var query = _context.OrderItems.Include(i => i.Product).Where(i=>i.PurchaseOrderId==OrderId).AsQueryable();
            var data = await query
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            var res = new PagedList<OrderItem>();
            res.Data = data;
            res.TotalCount = query.Count();
            return res;
        }

        public async Task<int> Update(OrderItem request)
        {
            var old = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<OrderItem, OrderItem>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
