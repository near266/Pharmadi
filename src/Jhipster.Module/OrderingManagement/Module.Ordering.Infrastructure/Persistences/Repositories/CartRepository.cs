using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Infrastructure.Persistences;
using Jhipster.Service.Utilities;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Shared.DTOs;

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

        public async Task<int> Delete(List<Guid> ids)
        {
            foreach(var id in ids)
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


        public async Task<PagedList<ViewCartByBrandDTO>> GetAllByUser(int page, int pageSize, Guid userId)
        {

            var query = _context.Carts.Where(c => c.UserId == userId).Select(c => new Cart
            {
                Id = c.Id,
                UserId = c.UserId,
                Product = _context.Products.Where(i => i.Id == c.ProductId)
                                .Include(i => i.TagProducts).ThenInclude(i => i.Tag)
                                .Include(i => i.LabelProducts).ThenInclude(i => i.Label)
                                .FirstOrDefault(),
                Quantity = c.Quantity
            }).AsQueryable();


            //var query1 = query.GroupBy(i => i.Product.Brand).Select(i => new ViewCartByBrandDTO
            //{
            //    Brand = i.Key,
            //    Carts = i.ToList()
            //}).AsQueryable();

            //var query2 = query1.ToListAsync();
            //var data = await query1
            //            .Skip(pageSize * (page - 1))
            //            .Take(pageSize).ToListAsync();
            //var res = new PagedList<ViewCartByBrandDTO>();
            //res.Data = data;
            //res.TotalCount = query1.Select(i => i.Brand).Count();
            //return res;
            var res = new List<ViewCartByBrandDTO>();
            var q1 = await _context.Carts.Include(i => i.Product).Where(i=>i.UserId==userId).Select(i => i.Product.BrandId).Distinct().ToListAsync();
            foreach(var item in q1)
            {
                var temp = new ViewCartByBrandDTO
                {
                    Brand = _context.Brands.Where(i => i.Id == item).FirstOrDefault(),
                    Carts = query.Where(q => q.Product.BrandId == item).ToList()
                };
                res.Add(temp);
            }

            var data = res.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            var result = new PagedList<ViewCartByBrandDTO>();
            result.Data = data.AsEnumerable();
            result.TotalCount = res.Count();
            return result;
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

        public async Task<CartResultDTO> CartResultSum(Guid userId)
        {
            var res = new CartResultDTO();
            var data = await _context.Carts.Include(i=>i.Product).Where(i => i.UserId == userId && i.IsChoice == true).ToListAsync();
            res.Quantity = (int)data.Sum(i => i.Quantity);
            var summ = 0;
            foreach(var item in data)
            {
                summ = (int)(item.Quantity * item.Product.SalePrice);
                res.TotalPrice += summ;
            }
            return res;
            
        }
    }
}
