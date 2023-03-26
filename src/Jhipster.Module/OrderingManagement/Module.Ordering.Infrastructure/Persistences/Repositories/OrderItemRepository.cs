using AutoMapper;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;



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

        public async Task<int> Delete(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var obj = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id.Equals(id));
                if (obj != null)
                {
                    _context.OrderItems.Remove(obj);
                    await _context.SaveChangesAsync();
                }
            }

            return 1;
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

        public async Task<PagedList<OrderItem>> GetAllItemByOrder(int page, int pageSize, Guid OrderId)
        {
            var query = _context.OrderItems.Where(c => c.PurchaseOrderId == OrderId).Select(c => new OrderItem
            {
                Id = c.Id,
                PurchaseOrderId = c.PurchaseOrderId,
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

            var res = new PagedList<OrderItem>();
            res.Data = data;
            res.TotalCount = query.Count();
            return res;
        }

        
    }
}
