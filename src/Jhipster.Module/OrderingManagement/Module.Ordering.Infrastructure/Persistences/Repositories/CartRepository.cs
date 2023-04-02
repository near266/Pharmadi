﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Shared.DTOs;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

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
            catch(Exception e)
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


        public async Task<ViewCartDTO> GetAllByUser(int page, int pageSize, Guid userId)
        {

            var view = new ViewCartDTO();
            var query =  _context.Carts.Where(c => c.UserId == userId).Select(c => new Cart
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
            }).OrderByDescending(i=>i.LastModifiedDate).AsQueryable();

            var res = new List<ViewCartByBrandDTO>();
            var q1 = new List<Guid>();
            foreach (var item in query)
            {
                var id = (Guid)item.Product.BrandId;
                if (!q1.Contains(id))
                {
                    q1.Add(id);
                }
            }
           
            foreach (var item in q1)
            {
                var temp = new ViewCartByBrandDTO
                {
                    Brand = _context.Brands.Where(i => i.Id == item).FirstOrDefault(),
                    Carts = query.Where(q => q.Product.BrandId == item).OrderByDescending(i=>i.LastModifiedDate).ToList()
                };
                res.Add(temp);
            }

            var data = res.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            var totalPrice = _context.Carts.Where(i => i.UserId == userId).Sum(i => i.Product.Price);
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
        public async Task<List<ViewQuickOrder>> ViewQuick(Guid userId)
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
                           Price = s.Price,
                           DiscountPrice = s.SalePrice,
                           BrandName = sst.BrandName,
                           Quantity = st.Quantity ?? 0
                       };
            return data.ToList();

        }
        public async Task<CartResultDTO> CartResultSum(Guid userId)
        {
            var res = new CartResultDTO();
            var data = await _context.Carts.Include(i => i.Product).Where(i => i.UserId == userId && i.IsChoice == true).ToListAsync();
            res.Quantity = (int)data.Sum(i => i.Quantity);
            
            foreach (var item in data)
            {
                var summ = (int)(item.Quantity * item.Product.Price);
                res.TotalPrice += summ;

                var summ1 = (int)(item.Quantity * item.Product.SalePrice);
                res.TotalPayment += summ1;

            }
            return res;

        }

    }
}
