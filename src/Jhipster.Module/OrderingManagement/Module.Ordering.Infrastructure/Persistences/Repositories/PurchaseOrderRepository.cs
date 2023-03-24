using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Module.Ordering.Application.DTO;

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
            request.OrderCode = "DH001";
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

        public async Task<PagedList<PurchaseOrder>> GetAllAdmin(int page, int pageSize, int? status, DateTime? fromDate, DateTime? toDate, string? codekey, string? customerkey)
        {

            var query = _context.PurchaseOrders.Include(i => i.Merchant).AsQueryable();
            if(fromDate!=null)
            {
                query = query.Where(i=>i.CreatedDate>=fromDate);
            }
            if (toDate != null)
            {
                query = query.Where(i => i.CreatedDate <= toDate);
            }
            if (status!=null)
            {
                query = query.Where(i => i.Status == status);
            }
            if (codekey != null)
            {
                codekey = codekey.ToLower();
                query = query.Where(i => i.OrderCode.ToLower().Contains(codekey));
            }
            if (customerkey != null)
            {
                customerkey = customerkey.ToLower();
                query = query.Where(i => i.Merchant.MerchantName.ToLower().Contains(customerkey));
            }
            var data = await query
                        .Skip(pageSize *( page-1))
                        .Take(pageSize)
                        .ToListAsync();
            var result = new PagedList<PurchaseOrder>();
            result.Data = data;
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<PurchaseOrder> ViewDetail(Guid id)
        {

            var result = await _context.PurchaseOrders.Include(i => i.Merchant).FirstOrDefaultAsync();
            return result;
        }

        public async Task<PagedList<PurchaseOrder>> GetAllByUser(int page, int pageSize, int? status, Guid userId)
        {

            var query = _context.PurchaseOrders.Include(i=>i.Merchant).Where(i=>i.MerchantId==userId).AsQueryable();
            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }
            var data = await query
                        .Skip(pageSize *( page-1))
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
        public async Task<int> UpdateStatus(Guid Id, int Status)
        {
            var old = await _context.PurchaseOrders.FirstOrDefaultAsync(i => i.Id.Equals(Id));
            if (old != null)
            {

                old.Status = Status;

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }
        public async Task<List<HistoryOrderDTO>> transactionHistory(Guid id)
        {
            var data = from s in _context.PurchaseOrders
                       where s.MerchantId == id
                       join st in _context.OrderItems on s.Id equals st.PurchaseOrderId
                       select new HistoryOrderDTOs
                       {
                           MerchantId = s.MerchantId,
                           ShippingFee = s.ShippingFee,
                           TotalPrice = s.TotalPrice,
                           TotalPayment = s.TotalPayment,
                           Status = s.Status,
                           OrderItemId = st.Id,
                           QuantityOrderItem = st.Quantity,

                       };
            var value = data.GroupBy(i => new { i.MerchantId, i.ShippingFee, i.TotalPayment, i.TotalPrice, i.Status }).Select(g => new HistoryOrderDTO
            {
                MerchantId = g.Key.MerchantId,
                ShippingFee = g.Key.ShippingFee,
                TotalPrice = g.Key.TotalPrice,
                TotalPayment = g.Key.TotalPayment,
                Status = g.Key.Status,
                ToTalProduct = g.Sum(a => a.QuantityOrderItem),
                ToTalOrderItem = g.Count()
            });
            return value.ToList();
        }

    }
}
