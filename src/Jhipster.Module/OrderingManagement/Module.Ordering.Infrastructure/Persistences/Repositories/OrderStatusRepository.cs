using AutoMapper;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;



namespace Module.Ordering.Infrastructure.Persistence.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepostitory
    {
        private readonly OrderingDbContext _context;
        private readonly IMapper _mapper;
        public OrderStatusRepository(OrderingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(OrderStatus request)
        {
            await _context.OrderStatuses.AddAsync(request);
            return await _context.SaveChangesAsync();
           
        }

        public async Task<IEnumerable<OrderStatus>> GetAll(Guid OrderId)
        {
            var query = _context.OrderStatuses.Where(x => x.PurchaseOrderId == OrderId).AsEnumerable();
            return query;
        }

        
    }
}
