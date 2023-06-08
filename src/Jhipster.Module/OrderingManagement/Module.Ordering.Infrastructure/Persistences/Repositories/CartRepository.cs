using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Shared.DTOs;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            try
            {
                var checkPrduct = await _context.Products.FirstOrDefaultAsync(i => i.Status == 2 && i.Archived == false && i.CanOrder == true && i.Id == request.ProductId);
                if (checkPrduct == null) throw new Exception("Sản phẩm đang đợi kiểm duyệt, không cho phép đặt hàng ");
                else
                {
                    var obj = await _context.Carts.FirstOrDefaultAsync(i => i.UserId == request.UserId && i.ProductId == request.ProductId);
                    if (obj != null)
                    {
                        request.Id = obj.Id;
                        request.Quantity = obj.Quantity + request.Quantity;
                        if (request.Quantity == 0)
                        {
                            _context.Carts.Remove(obj);
                            return await _context.SaveChangesAsync();
                        }
                        obj = _mapper.Map<Cart, Cart>(request, obj);

                        return await _context.SaveChangesAsync(default);
                    }
                    await _context.Carts.AddAsync(request);
                    return await _context.SaveChangesAsync();
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
        public async Task<int> Delete(List<Guid> ids)
        {
            foreach (var id in ids)
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


        public async Task<ViewCartDTO> GetAllByUser(int page, int pageSize, Guid userId, int? check)
        {

            var view = new ViewCartDTO();

            var res = new List<ViewCartByBrandDTO>();
            var q1 = new List<Guid>();
            if (check == null || check == 0)
            {
                var query = _context.Carts.Where(c => c.UserId == userId).Select(c => new Cart
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Product = _context.Products.Where(i => i.Id == c.ProductId)
                              //.Include(i => i.TagProducts).ThenInclude(i => i.Tag)
                              .Include(i => i.LabelProducts).ThenInclude(i => i.Label)
                              .FirstOrDefault(),
                    LastModifiedDate = c.LastModifiedDate,
                    Quantity = c.Quantity,
                    IsChoice = c.IsChoice
                }).OrderByDescending(i => i.LastModifiedDate).AsQueryable();

                foreach (var item in query)
                {
                    var id = (Guid)item.Product.BrandId;
                    if (!q1.Contains(id))
                    {
                        q1.Add(id);
                    }
                }
                foreach (var item in query)
                {
                    if (item.Product.ShortName == null)
                    {
                        item.Product.ShortName = item.Product.ProductName.Substring(0, 25);
                    }
                }

                foreach (var item in q1)
                {
                    var temp = new ViewCartByBrandDTO
                    {
                        Brand = _context.Brands.Where(i => i.Id == item).FirstOrDefault(),
                        Carts = query.Where(q => q.Product.BrandId == item).OrderByDescending(i => i.LastModifiedDate).ToList()
                    };
                    res.Add(temp);
                }
            }
            else
            {
                var query1 = _context.Carts.Where(c => c.UserId == userId).Select(c => new Cart
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Product = _context.Products.Where(i => i.Id == c.ProductId)
                              //.Include(i => i.TagProducts).ThenInclude(i => i.Tag)
                              .Include(i => i.LabelProducts).ThenInclude(i => i.Label)
                              .FirstOrDefault(),
                    LastModifiedDate = c.LastModifiedDate,
                    Quantity = c.Quantity,
                    IsChoice = c.IsChoice
                }).AsQueryable();

                foreach (var item in query1)
                {
                    var id = (Guid)item.Product.BrandId;
                    if (!q1.Contains(id))
                    {
                        q1.Add(id);
                    }
                }
                foreach (var item in query1)
                {
                    if (item.Product.ShortName == null)
                    {
                        item.Product.ShortName = item.Product.ProductName.Substring(0, 25);
                    }
                }
                foreach (var item in q1)
                {
                    var temp = new ViewCartByBrandDTO
                    {
                        Brand = _context.Brands.Where(i => i.Id == item).FirstOrDefault(),
                        Carts = query1.Where(q => q.Product.BrandId == item).ToList()
                    };
                    res.Add(temp);
                }
            }
            var data = res.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            var totalPrice = _context.Carts.Where(i => i.UserId == userId).Sum(i => i.Product.SuggestPrice);
            var totalDiscount = _context.Carts.Where(i => i.UserId == userId).Sum(i => i.Product.SalePrice);


            view.data = data;
            view.TotalCount = res.Count();
            view.TotalPrice = (decimal)totalPrice;
            view.TotalDiscount = (decimal)totalDiscount;
            view.EconomicalPrice = (decimal)totalPrice - (decimal)totalDiscount;
            return view;
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
        public async Task<List<ViewQuickOrder>> ViewQuick(Guid userId, int Type, string keyword)
        {
            if (Type == 1)
            {
                var data = from s in _context.Products
                           join sst in _context.Brands on s.BrandId equals sst.Id
                           join st in _context.Carts.Where(a => a.UserId == userId) on s.Id equals st.ProductId into cart
                           from st in cart.DefaultIfEmpty()
                           select new ViewQuickOrder()
                           {
                               ProductId = s.Id,
                               ProductName = s.ProductName,
                               Image = s.Image,
                               Price = s.SuggestPrice,
                               DiscountPrice = s.SalePrice,
                               BrandName = sst.BrandName,
                               Quantity = st.Quantity ?? 0,
                               ShortName = s.ShortName != null ? s.ShortName : s.ProductName.Substring(0, 25)
                           };
                var check = data.AsQueryable();
                check = keyword != null ? check.Where(i => i.ProductName.Contains(keyword)) : check;
                return check.ToList();
            }
            if (Type == 2) // sản phẩm đã mua
            {
                var data = from s in _context.PurchaseOrders.Where(a => a.MerchantId == userId)
                           join st in _context.OrderItems on s.Id equals st.PurchaseOrderId
                           join sst in _context.Products on st.ProductId equals sst.Id
                           join brand in _context.Brands on sst.BrandId equals brand.Id
                           join carts in _context.Carts.Where(a => a.UserId == userId) on sst.Id equals carts.ProductId into cart
                           from carts in cart.DefaultIfEmpty()
                           select new ViewQuickOrder()
                           {
                               ProductId = st.ProductId,
                               ProductName = sst.ProductName,
                               Image = sst.Image,
                               Price = sst.SuggestPrice,
                               DiscountPrice = sst.SalePrice,
                               BrandName = brand.BrandName,
                               Quantity = carts.Quantity ?? 0,
                               ShortName = sst.ShortName != null ? sst.ShortName : sst.ProductName.Substring(0, 25)
                           };
                var check = data.AsQueryable();
                check = keyword != null ? check.Where(i => i.ProductName.Contains(keyword)) : check;
                return check.ToList();
            }
            if (Type == 3) // sản phẩm trong nước
            {
                var data = from s in _context.Products.Where(a => a.Country.ToLower().Equals("vietnam"))
                           join st in _context.Brands on s.BrandId equals st.Id
                           join carts in _context.Carts.Where(a => a.UserId == userId) on s.Id equals carts.ProductId into cart
                           from carts in cart.DefaultIfEmpty()
                           select new ViewQuickOrder()
                           {
                               ProductId = s.Id,
                               ProductName = s.ProductName,
                               Image = s.Image,
                               Price = s.SuggestPrice,
                               DiscountPrice = s.SalePrice,
                               BrandName = st.BrandName,
                               Quantity = carts.Quantity ?? 0,
                               ShortName = s.ShortName != null ? s.ShortName : s.ProductName.Substring(0, 25)
                           };
                var check = data.AsQueryable();
                check = keyword != null ? check.Where(i => i.ProductName.Contains(keyword)) : check;
                return check.ToList();
            }
            else // sản phẩm nước ngoài
            {

                var data = from s in _context.Products.Where(a => a.Country.ToLower() != "vietnam")
                           join st in _context.Brands on s.BrandId equals st.Id
                           join carts in _context.Carts.Where(a => a.UserId == userId) on s.Id equals carts.ProductId into cart
                           from carts in cart.DefaultIfEmpty()
                           select new ViewQuickOrder()
                           {
                               ProductId = s.Id,
                               ProductName = s.ProductName,
                               Image = s.Image,
                               Price = s.SuggestPrice,
                               DiscountPrice = s.SalePrice,
                               BrandName = st.BrandName,
                               Quantity = carts.Quantity ?? 0,
                               ShortName = s.ShortName != null ? s.ShortName : s.ProductName.Substring(0, 25)
                           };
                var check = data.AsQueryable();
                check = keyword != null ? check.Where(i => i.ProductName.Contains(keyword)) : check;
                return check.ToList();
            }

        }
        public async Task<CartResultDTO> CartResultSum(Guid userId)
        {
            var res = new CartResultDTO();
            res.TotalPrice = 0;
            res.TotalPayment = 0;
            var data = await _context.Carts.Include(i => i.Product).Where(i => i.UserId == userId && i.IsChoice == true).ToListAsync();
            res.Quantity = (int)data.Sum(i => i.Quantity);

            foreach (var item in data)
            {
                var checkPrice = item.Product.SuggestPrice != null ? item.Product.SuggestPrice : item.Product.SalePrice;
                // tính Giá theo từng sản phẩm 
                var dis = ProductDiscount(item.UserId, item.ProductId);
                if (dis.unit == "%")
                {
                    var summ = item.Quantity * item.Product.SalePrice;
                    res.TotalPrice += summ;
                    var summ1 = item.Quantity * item.Product.SalePrice * (decimal)(100 - dis.Discount) / 100;
                    res.TotalPayment += summ1;
                }
                if (dis.unit.ToLower() == "VNĐ".ToLower())
                {
                    var summ = item.Quantity * item.Product.SalePrice;
                    res.TotalPrice += summ;
                    var summ1 = item.Quantity * item.Product.SalePrice - (decimal)(dis.Discount);
                    res.TotalPayment += summ1;
                }
                if (dis.unit == "0")
                {
                    var summ = item.Quantity * item.Product.SalePrice;
                    res.TotalPrice += summ;
                    var summ1 = item.Quantity * item.Product.SalePrice;
                    res.TotalPayment += summ1;
                }
            }
            res.economicalPrice = res.TotalPrice - res.TotalPayment;
            return res;

        }
        private DiscountDTO ProductDiscount(Guid? userId, Guid? productId)
        {
            var s = new DiscountDTO();
            var checkCart = _context.Carts.Where(i => i.UserId == userId && i.ProductId == productId).FirstOrDefault().Quantity;
            var checkProduct = _context.productDiscounts.Where(i => i.ProductId == productId).OrderBy(i => i.Max);
            if (checkProduct.Any())
            {
                foreach (var item in checkProduct)
                {
                    if (item.Min > checkCart)
                    {
                        s.Discount = 0;
                        s.unit = "0";
                        break;
                    }
                    if (item.Min <= checkCart && checkCart <= item.Max)
                    {
                        s.Discount = item.Discount;
                        s.unit = item.Unit;
                        break;
                    }

                }
                if (s.unit == null)
                {
                    s.Discount = checkProduct.OrderByDescending(i => i.Max).FirstOrDefault().Discount;
                    s.unit = checkProduct.FirstOrDefault().Unit;
                }
            }
            else
            {
                s.Discount = 0;
                s.unit = "0";
            }
            return s;
        }
        private class DiscountDTO
        {
            public float Discount { get; set; }

            public string unit { get; set; }
        }
    }
}
