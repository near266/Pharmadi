using AutoMapper;
using Jhipster.Service.Utilities;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Module.Ordering.Shared.DTOs;

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
            var res = 0;
            foreach (var id in ids)
            {
                var obj = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id.Equals(id));
                if (obj != null)
                {
                    _context.OrderItems.Remove(obj);
                    res = await _context.SaveChangesAsync();
                }
            }

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

        public async Task<PagedList<OrderItemResponse>> GetAllItemByOrder(Guid OrderId)
        {
            var query = _context.OrderItems.Where(c => c.PurchaseOrderId == OrderId).Select(c => new OrderItem
            {
                Id = c.Id,
                PurchaseOrderId = c.PurchaseOrderId,
                Product = _context.Products.Where(i => i.Id == c.ProductId)
                                 .Include(i => i.LabelProducts).ThenInclude(i => i.Label)
                                 .FirstOrDefault(),
                Quantity = c.Quantity,
                ProductId = c.ProductId
            }).AsQueryable();

            var res = new PagedList<OrderItemResponse>();

            var data = new List<OrderItemResponse>();
            var q1 = await query.Select(i => i.Product.BrandId).Distinct().ToListAsync();
            foreach (var item in q1)
            {
                var temp = new OrderItemResponse
                {
                    Brand = _context.Brands.Where(i => i.Id == item).FirstOrDefault(),
                    OrderItems = query.Where(q => q.Product.BrandId == item).ToList(),
                    BrandQuantity = query.Where(q => q.Product.BrandId == item).Select(i=>i.ProductId).Distinct().Count(),
                };
                data.Add(temp);
            }
            res.Data = data.AsEnumerable();
            res.TotalCount = query.Select(i => i.ProductId).Distinct().Count();
            return res;
        }

        public async Task<PagedList<OrderItem>> GetAllItemByOrderAdmin(Guid OrderId)
        {
            var query = await _context.OrderItems.Where(c => c.PurchaseOrderId == OrderId).Select( c => new OrderItem
            {
                Id = c.Id,
                PurchaseOrderId = c.PurchaseOrderId,
                Product =   _context.Products.Where(i => i.Id == c.ProductId)
                                 .Include(i => i.Brand)
                                 .Include(i => i.LabelProducts).ThenInclude(i => i.Label)
                                 .FirstOrDefault(),
                Quantity = c.Quantity
            }).ToListAsync();

            var res = new PagedList<OrderItem>();
            res.Data = query;
            res.TotalCount = query.Count();
            return res;
        }




    }
}
