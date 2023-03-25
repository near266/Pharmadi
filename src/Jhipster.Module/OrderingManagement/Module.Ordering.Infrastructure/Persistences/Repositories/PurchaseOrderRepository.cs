using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Module.Ordering.Application.DTO;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Jhipster.Helpers;

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
            var maxCode = MaxNumberOrderCurrent($"ĐFH{DateTime.Now.Year}_");
            int number = 0;
            try
            {
                string[] words = maxCode.Split('_');
                number = int.Parse(words[1]);
            }
            catch
            {
                number = 1;
            }

            int yearnow = DateTime.Now.Year;
            var shortCode = ShortIdHelper.GenerateCode(ShortIdHelper.ObjectConstant.order, yearnow, number + 1);

            request.OrderCode = shortCode;
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
            if (fromDate != null)
            {
                query = query.Where(i => i.CreatedDate >= fromDate);
            }
            if (toDate != null)
            {
                query = query.Where(i => i.CreatedDate <= toDate);
            }
            if (status != null)
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
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            var result = new PagedList<PurchaseOrder>();
            result.Data = data;
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<PurchaseOrder> ViewDetail(Guid id)
        {

            var result = await _context.PurchaseOrders.Where(i=>i.Id==id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<PagedList<PurchaseOrder>> GetAllByUser(int page, int pageSize, int? status, Guid userId)
        {

            var query = _context.PurchaseOrders.Where(i => i.MerchantId == userId).AsQueryable();
            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }
            var data = await query
                        .Skip(pageSize * (page - 1))
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
        public async Task<PagedList<HistoryOrderDTO>> transactionHistory(Guid id, int? type, int? Status, string? OrderCode, string? productKey, DateTime? fromDate, DateTime? toDate, int page, int pageSize)
        { 
            var dataPurchaseOrders = _context.PurchaseOrders.Include(i=>i.OrderItems).AsQueryable();
            //var dataProduct = _context.Products.AsQueryable();
            dataPurchaseOrders = id != null ? dataPurchaseOrders.Where(i => i.MerchantId == id) : dataPurchaseOrders;

            if (type ==1)
            {
                dataPurchaseOrders = OrderCode != null ? dataPurchaseOrders.Where(i => i.OrderCode.ToLower().Contains(OrderCode.ToLower())) : dataPurchaseOrders;
            }
            else if(type ==2)
            {
                dataPurchaseOrders = productKey != null ? dataPurchaseOrders.Where(i => i.OrderItems.Any(i=>i.Product.ProductName.ToLower().Contains(productKey.ToLower()))) : dataPurchaseOrders;
            }
            else
            {
                dataPurchaseOrders = fromDate != null ? dataPurchaseOrders.Where(i => i.CreatedDate >= fromDate) : dataPurchaseOrders;
                dataPurchaseOrders = toDate != null ? dataPurchaseOrders.Where(i => i.CreatedDate <= toDate) : dataPurchaseOrders;
            }

            dataPurchaseOrders = Status != null ? dataPurchaseOrders.Where(i => i.Status == Status) : dataPurchaseOrders;

            var data = dataPurchaseOrders.Select(i => new HistoryOrderDTO
            {
                OrderCode = i.OrderCode,
                CreateDate = i.CreatedDate,
                TotalPayment = i.TotalPayment,
                ToTalProduct = i.OrderItems.Where(o => o.PurchaseOrderId == i.Id).Select(i => i.ProductId).Distinct().Count(),
                ToTalOrderItem = i.OrderItems.Where(o => o.PurchaseOrderId == i.Id).Select(i => i.Quantity).Sum(),
                Status = i.Status,
            });
            
           var res =await  data.Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToListAsync();
            var result = new PagedList<HistoryOrderDTO>();
            result.Data = res.AsEnumerable();
            result.TotalCount = data.Count();
            return result;
        }

        public string MaxNumberOrderCurrent(string staticCode)
        {
            var code =  _context.PurchaseOrders.Where(i => i.OrderCode.Contains(staticCode)).OrderByDescending(i => i.OrderCode).FirstOrDefault();
            if (code == null) return string.Empty;
            return code.OrderCode;
        }


    }
}
