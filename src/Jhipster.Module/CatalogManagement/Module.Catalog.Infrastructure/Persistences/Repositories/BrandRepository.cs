using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Module.Catalog.Shared.DTOs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text;

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
            var productBrand = await _context.Products.Where(i => i.BrandId == obj.Id).ToListAsync();

            if (obj != null)
            {
                if (productBrand != null && productBrand.Count == 0)
                {
                    _context.Products.RemoveRange(productBrand);
                    await _context.SaveChangesAsync();
                }
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

        public async Task<IEnumerable<BrandDTO>> Search(string? keyword)
        {
            var query = _context.Brands.AsQueryable();
            var result = await query.Where(i => i.Archived == false).Include(i => i.GroupBrand).Select(i => new BrandDTO
            {
                Id = i.Id,
                GroupBrandId = i.GroupBrandId,
                Intro = i.Intro,
                BrandName = i.BrandName,
                LogoBrand = i.LogoBrand,
                Pin = i.Pin,
                GroupBrand = i.GroupBrand,
                SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).Count(),
                products = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).AsEnumerable()

            })
              .ToListAsync();
            if (keyword != null)
            {
                keyword = keyword.ToLower();

                var data = await query.Where(i => i.BrandName.ToLower().Contains(keyword) && i.Archived == false).Include(i => i.GroupBrand).Select(i => new BrandDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).Count(),
                    products = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).AsEnumerable()

                })
              .ToListAsync();

                return data;
            }
            return result;
        }

        public async Task<PagedList<BrandDTO>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<BrandDTO>();
            var query = _context.Brands.AsQueryable();
            var data = await query.Where(i => i.Archived == false).OrderByDescending(i=>i.LastModifiedDate).Include(i => i.GroupBrand).Select(i => new BrandDTO
            {

                Id = i.Id,
                GroupBrandId = i.GroupBrandId,
                Intro = i.Intro,
                BrandName = i.BrandName,
                LogoBrand = i.LogoBrand,
                Pin = i.Pin,
                GroupBrand = i.GroupBrand,
                //SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).Count(),
               // products = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).AsEnumerable()



            }).Skip(pageSize * (page - 1))
                 .Take(pageSize).ToListAsync();
            foreach(var item in data)
            {
                var product =await _context.Products.Where(a => a.BrandId == (Guid?)item.Id && a.Archived == false).ToListAsync();
                item.SumProduct = product.Count();
            }    
            result.Data = data;
            result.TotalCount = query.Count();
            return result;
        }

        public async Task<int> PinBrand(Brand brand)
        {
            var old = await _context.Brands.FirstOrDefaultAsync(i => i.Id == brand.Id);
            if (old != null)
            {
                old.LastModifiedDate = DateTime.UtcNow;
                old.LastModifiedBy = brand.LastModifiedBy;
                old.Pin = brand.Pin;
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }
        public string Price(string? sup)
        {

            StringBuilder sb = new StringBuilder(sup);
            char replaceChar = 'x';
            for (int i = 1; i < sb.Length; i++)
            {
                sb[i] = replaceChar;
            }
            return sb.ToString();
        }
        public async Task<PagedList<IsHaveGroupDTO>> IsHaveGroup(int page, int pageSize, int type, Guid? GroupBrandId, Guid? UserId)
        {
            var query = _context.Brands.AsNoTracking();
            var result = new PagedList<IsHaveGroupDTO>();
            if (type == 1)
            {
                var brId = await _context.Brands.Where(i => i.GroupBrandId == GroupBrandId && i.Archived == false).Select(i => i.Id).ToListAsync();

                var data = await query.Where(i => i.GroupBrandId != null && i.GroupBrandId == GroupBrandId && i.Archived == false).Include(i => i.GroupBrand).Select(i => new IsHaveGroupDTO
                {

                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(i => brId.Contains((Guid)i.BrandId) && i.Archived == false).Count(),
                    products = _mapper.Map<List<ProductDetail>>(_context.Products.Where(i => brId.Contains((Guid)i.BrandId) && i.Archived == false).OrderByDescending(i => i.LastModifiedDate).ToList())



                }).ToListAsync();
                foreach (var item in data)
                {
                    foreach (var produt in item.products)
                    {
                        if (UserId == null)
                        {
                            produt.SalePrice = Price(produt.SalePrice);
                            produt.SuggestPrice = Price(produt.SuggestPrice);
                        }
                        else
                        {

                            produt.SalePrice = produt.SalePrice;
                            produt.SuggestPrice = produt.SuggestPrice;
                        }
                    }
                }
                result.Data = data.Skip(pageSize * (page - 1))
                  .Take(pageSize);
                result.TotalCount = data.Count();
                return result;
            }
            else if (type == 2)
            {
                var brId = await _context.Brands.Where(i => i.GroupBrandId == null && i.Archived == false).Select(i => i.Id).ToListAsync();
                var data = query.Where(i => i.GroupBrandId == null && i.Archived == false).Include(i => i.GroupBrand).Select(i => new IsHaveGroupDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).Count(),
                    //    products = _mapper.Map<List<ProductDetail>>(_context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).OrderByDescending(i => i.LastModifiedDate).ToList())

                });
                var datapro = await data.ToListAsync();
                //foreach (var item in datapro)
                //{
                //    foreach (var produt in item.products)
                //    {
                //        if (UserId == null)
                //        {
                //            produt.SalePrice = Price(produt.SalePrice);
                //            produt.SuggestPrice = Price(produt.SuggestPrice);
                //        }
                //        else
                //        {

                //            produt.SalePrice = produt.SalePrice;
                //            produt.SuggestPrice = produt.SuggestPrice;
                //        }
                //    }
                //}
                result.Data = datapro.Skip(pageSize * (page - 1))
                  .Take(pageSize);
                result.TotalCount = datapro.Count();
                return result;
            }
            else if (type == 3)
            {
                var brId =await _context.Brands.Where(i => i.Pin == true && i.Archived == false).Select(i => i.Id).ToListAsync();
                var data = await query.Where(i => i.Pin == true && i.Archived == false).Include(i => i.GroupBrand).Select(i => new IsHaveGroupDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).Count(),
                    products = _mapper.Map<List<ProductDetail>>(_context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false).OrderByDescending(i => i.LastModifiedDate).ToList())
                }).ToListAsync();
                foreach (var item in data)
                {
                    foreach (var produt in item.products)
                    {
                        if (UserId == null)
                        {
                            produt.SalePrice = Price(produt.SalePrice);
                            produt.SuggestPrice = Price(produt.SuggestPrice);
                        }
                        else
                        {

                            produt.SalePrice = produt.SalePrice;
                            produt.SuggestPrice = produt.SuggestPrice;
                        }
                    }
                }
                result.Data = data.Skip(pageSize * (page - 1))
                  .Take(pageSize);
                result.TotalCount = data.Count();
                return result;
            }
            else if (type == 4)
            {
                var listpro =await _context.Products.Where(i => i.Country.ToLower() == "VIỆT NAM".ToLower() && i.Archived == false).Select(i => i.BrandId).Distinct().ToListAsync();
                var pro =await _context.Products.Where(i => i.Country.ToLower() == "VIỆT NAM".ToLower() && i.Archived == false).Select(i => i.Id).ToListAsync();
                var brId =await _context.Brands.Where(i => listpro.Contains(i.Id) && i.Archived == false).Select(i => i.Id).ToListAsync();
                var data = await query.Where(i => i.Archived == false && listpro.Contains(i.Id)).Include(i => i.GroupBrand).Select(i => new IsHaveGroupDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false && pro.Contains(a.Id)).Count(),
                    products = _mapper.Map<List<ProductDetail>>(_context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false && pro.Contains(a.Id)).OrderByDescending(i => i.LastModifiedDate).ToList())
                }).ToListAsync();
                foreach (var item in data)
                {
                    foreach (var produt in item.products)
                    {
                        if (UserId == null)
                        {
                            produt.SalePrice = Price(produt.SalePrice);
                            produt.SuggestPrice = Price(produt.SuggestPrice);
                        }
                        else
                        {

                            produt.SalePrice = produt.SalePrice;
                            produt.SuggestPrice = produt.SuggestPrice;
                        }
                    }
                }
                result.Data = data.Skip(pageSize * (page - 1))
                  .Take(pageSize);
                result.TotalCount = data.Count();
                return result;
            }
            else if (type == 5)
            {
                var listpro =await _context.Products.Where(i => i.Country.ToLower() != "VIỆT NAM".ToLower() && i.Archived == false).Select(i => i.BrandId).Distinct().ToListAsync();
                var pro =await _context.Products.Where(i => i.Country.ToLower() != "VIỆT NAM".ToLower() && i.Archived == false).Select(i => i.Id).ToListAsync();
                var brId =await _context.Brands.Where(i => listpro.Contains(i.Id) && i.Archived == false).Select(i => i.Id).ToListAsync();
                var data = await query.Where(i => i.Archived == false && listpro.Contains(i.Id)).Include(i => i.GroupBrand).Select(i => new IsHaveGroupDTO
                {
                    Id = i.Id,
                    GroupBrandId = i.GroupBrandId,
                    Intro = i.Intro,
                    BrandName = i.BrandName,
                    LogoBrand = i.LogoBrand,
                    Pin = i.Pin,
                    GroupBrand = i.GroupBrand,
                    SumProduct = _context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false && pro.Contains(a.Id)).Count(),
                    products = _mapper.Map<List<ProductDetail>>(_context.Products.Where(a => a.BrandId == (Guid?)i.Id && i.Archived == false && pro.Contains(a.Id)).OrderByDescending(i => i.LastModifiedDate).ToList())
                }).ToListAsync();
                foreach (var item in data)
                {
                    foreach (var produt in item.products)
                    {
                        if (UserId == null)
                        {
                            produt.SalePrice = Price(produt.SalePrice);
                            produt.SuggestPrice = Price(produt.SuggestPrice);
                        }
                        else
                        {

                            produt.SalePrice = produt.SalePrice;
                            produt.SuggestPrice = produt.SuggestPrice;
                        }
                    }
                }
                result.Data = data.Skip(pageSize * (page - 1))
                  .Take(pageSize);
                result.TotalCount = data.Count();
                return result;
            }


            return result;

        }
        public async Task<List<string>> ImageBrand()
        {
            var s = await _context.Brands.OrderByDescending(i => i.Pin).Select(i => i.LogoBrand).Take(12).ToListAsync();
            return s;
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
            var br = await _context.Brands.FirstOrDefaultAsync(i => i.Id == Id);
            var gr = await _context.GroupBrands.FirstOrDefaultAsync(i => i.Id == br.GroupBrandId);
            if (gr == null)
            {
                return true;

            };
            return false;
        }

        public async Task<List<Guid>> GetListBrandByGroupId(Guid? Id)
        {
            var brand = await _context.Brands.Where(i => i.GroupBrandId == Id).Select(i => i.Id).ToListAsync();

            return brand;
        }

        public async Task<int> ArchiveBrand(Guid? Id)
        {
            var res = 0;


            var obj = await _context.Brands.FirstOrDefaultAsync(i => i.Id.Equals(Id));
            if (obj != null)
            {
                obj.Archived = true;
                obj.GroupBrandId = null;
                res = _context.SaveChanges();
            }


            return res;
        }

        public async Task<DetailBrand> BrandDetail(Guid Id)
        {
            var query = _context.Brands.AsQueryable();
            var Pr = await _context.Products.Where(i => i.BrandId == Id).Select(i => i.Id).ToListAsync();
            var cate = await _context.CategoryProducts.Where(i => Pr.Contains(i.ProductId) && i.Priority == true).Select(i => i.Category)
                .Where(a => a.ParentId == null).Select(a => a.Id).ToListAsync();
            var catename = await _context.Categories.Where(i => cate.Contains(i.Id)).OrderBy(a => a.CategoryName).Select(i => i.CategoryName).ToListAsync();

            var data = await query.Where(i => i.Id == Id).Select(i => new DetailBrand
            {
                Id = i.Id,
                GroupBrandId = i.GroupBrandId,
                Intro = i.Intro,
                BrandName = i.BrandName,
                LogoBrand = i.LogoBrand,
                Pin = i.Pin,
                CateName = catename,
                GroupBrand = i.GroupBrand,


            }).OrderBy(i => i.CateName).FirstOrDefaultAsync();

            return data;

        }
    }
}
