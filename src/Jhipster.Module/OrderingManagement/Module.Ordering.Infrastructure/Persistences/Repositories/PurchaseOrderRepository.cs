using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;

namespace Module.Factor.Infrastructure.Persistence.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepostitory
    {
        private readonly OrderingDbContext _context;
        private readonly IMapper _mapper;
        public PurchaseOrderRepository(OrderingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(PurchaseOrder request)
        {
            await _context.PurchaseOrders.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.PurchaseOrders.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.PurchaseOrders.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<PagedList<PurchaseOrder>> GetAllAdmin(int page, int pageSize, int? status)
        {

            var query = _context.PurchaseOrders.Include(i => i.OrderItems).AsQueryable();
            if(status!=null)
            {
                query = query.Where(i => i.Status == status);
            }
            var data = await query
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            var result = new PagedList<PurchaseOrder>();
            result.Data = data;
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<PurchaseOrder> ViewDetail(Guid id)
        {

            var result = await _context.PurchaseOrders.Include(i => i.Id==id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<PagedList<PurchaseOrder>> GetAllByUser(int page, int pageSize, int? status, Guid userId)
        {

            var query = _context.PurchaseOrders.Where(i=>i.MerchantId==userId).AsQueryable();
            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }
            var data = await query
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            var result = new PagedList<PurchaseOrder>();
            result.Data = data;
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<int> Update(PurchaseOrder request)
        {
            var old = await _context.PurchaseOrders.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<PurchaseOrder, PurchaseOrder>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
    }
}
