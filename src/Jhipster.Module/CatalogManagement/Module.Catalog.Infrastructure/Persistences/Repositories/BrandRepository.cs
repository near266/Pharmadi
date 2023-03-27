using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Module.Catalog.Shared.DTOs;
using Org.BouncyCastle.Crypto;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public BrandRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Brand request)
        {
            await _context.Brands.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Brands.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (obj != null)
            {
                _context.Brands.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(Brand request)
        {
            var old = await _context.Brands.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Brand, Brand>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }

        public async Task<IEnumerable<Brand>> Search(string? keyword)
        {
            var query = _context.Brands.AsQueryable();
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                query = query.Where(i => i.BrandName.ToLower().Contains(keyword));
            }
            var result = query.AsEnumerable();
            return result;
        }

        public async Task<PagedList<Brand>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Brand>();
            var query1 = _context.Brands.AsQueryable();
            var data = await query1
                        .Skip(pageSize * (page-1))
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<int> PinBrand(Brand brand)
        {
            var old = await _context.Brands.FirstOrDefaultAsync(i=>i.Id==brand.Id);
            if (old != null)
            {
                old.LastModifiedDate= DateTime.UtcNow;
                old.LastModifiedBy=brand.LastModifiedBy; 
                old.Pin= brand.Pin;
                _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<PagedList<BrandDTO>> IsHaveGroup(int page, int pageSize, int type, Guid? GroupBrandId)
        {
            var query = _context.Brands.AsQueryable();
            var result = new PagedList<BrandDTO>();
            if (type == 1)
            {
                var data = await query.Where(i => i.GroupBrandId != null&& i.GroupBrandId==GroupBrandId).Include(i => i.GroupBrand).Select(i => new BrandDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(i => i.BrandId == i.Id).Count()

                }).Skip(pageSize * (page - 1))
                  .Take(pageSize).ToListAsync();
                result.Data = data;
                result.TotalCount = data.Count();
                return result;
            }
            if (type == 2)
            {

                var data = await query.Where(i => i.GroupBrandId == null).Include(i => i.GroupBrand).Select(i => new BrandDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(i => i.BrandId == i.Id).Count()

                })
                    .Skip(pageSize * (page - 1))
                        .Take(pageSize).ToListAsync();
                result.Data = data;
                result.TotalCount = data.Count();
                return result;
            }
            return result;

        }
        public async Task<List<string>>ImageBrand()
        {
            return await _context.Brands.Select(i => i.LogoBrand).ToListAsync();
        }

        public async Task<int> AddListBrand(List<Brand> brands)
        {
            var result = 0;
            foreach (var brand in brands)
            {
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();
                result++;
            }
            return result;
        }

        public async Task<bool> IsBrandEmtyGroup(Guid? Id)
        {
           var br= await _context.Brands.FirstOrDefaultAsync(i=>i.Id==Id);
           var gr = await _context.GroupBrands.FirstOrDefaultAsync(i=>i.Id == br.GroupBrandId);
            if (gr == null) {
                return true;
            
            };
            return false;
        }

        public async Task<List<Guid>> GetListBrandByGroupId(Guid? Id)
        {
            var brand = await _context.Brands.Where(i=>i.GroupBrandId==Id).Select(i=>i.Id).ToListAsync();

            return brand;
        }

        public async Task<int> ArchiveBrand(Guid? Id)
        {
            var res = 0;
         
            
                var obj = await _context.Brands.FirstOrDefaultAsync(i => i.Id.Equals(Id));
                if (obj != null)
                {
                    obj.Archived = true;
                    res = await _context.SaveChangesAsync();
                }
           

            return res;
        }
    }
}
