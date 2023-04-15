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

namespace Module.Ordering.Infrastructure.Persistence.Repositories
{
    public class ProductSaleRepository : IProductSaleRepostitory
    {
        private readonly OrderingDbContext _context;
        private readonly IMapper _mapper;
        public ProductSaleRepository(OrderingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(ProductSale request)
        {
            try
            {
                var obj = await _context.ProductSales.FirstOrDefaultAsync(i =>i.ProductId == request.ProductId);
                if (obj != null)
                {
                    request.ProductId = obj.ProductId;
                    obj.Quantity = obj.Quantity + request.Quantity;

                    return await _context.SaveChangesAsync(default);
                }
                await _context.ProductSales.AddAsync(request);
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }
    }
}
