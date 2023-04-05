using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Infrastructure.Persistence.Repositories
{


    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(CatalogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Category request)
        {
            request.LastModifiedDate = request.CreatedDate;
            await _context.Categories.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(i => i.Id.Equals(id));
            var checkparent = await _context.Categories.Where(i => i.ParentId.Equals(id)).ToListAsync();
            if (obj != null) { return -1; }
            else
            {
                if (obj != null)
                {
                    _context.Categories.Remove(obj);
                    await _context.SaveChangesAsync();
                }
                if (checkparent != null || checkparent.Count() != 0)
                {
                    foreach (var item in checkparent)
                    {
                        _context.Categories.Remove(item);
                    }
                    await _context.SaveChangesAsync();
                }
                return 1;
            }
        }

        public async Task<int> Update(Category request)
        {
            var old = await _context.Categories.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {

                old = _mapper.Map<Category, Category>(request, old);

                return await _context.SaveChangesAsync(default);
            }
            return 0;
        }

        public async Task<PagedList<Category>> GetAllAdmin(int page, int pageSize)
        {
            var result = new PagedList<Category>();
            var query1 = _context.Categories.AsQueryable();
            var data = await query1
                        .OrderByDescending(x => x.LastModifiedDate)
                        .Skip(pageSize * page)
                        .Take(pageSize)
                        .ToListAsync();
            result.Data = data;
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<IEnumerable<Category>> Search(string? keyword)
        {
            var query = _context.Categories.AsQueryable();
            if(keyword != null)
            {

            keyword = keyword.ToLower();
            query =query.Where(i => i.CategoryName.ToLower().Contains(keyword));
            }
            var reslut = query.AsEnumerable();
            return reslut;
        }

        public async Task<IEnumerable<Category>> GetTwoLayer()
        {
            //var result = _context.Categories.Where(i=>i.ParentId==null).Select(i => new Category
            //{
            //    Id = i.Id,
            //    CategoryName = i.CategoryName,
            //    Categories = _context.Categories.Where(i=>i.ParentId==i.Id).AsEnumerable()

            //}).AsEnumerable();
            var result = _context.Categories.Where(i => i.ParentId == null).OrderBy(i=>i.CategoryName).AsEnumerable();
            foreach (var item in result)
            {
                item.Categories = _context.Categories.Where(i => i.ParentId == item.Id).OrderBy(i => i.CategoryName).AsEnumerable();
            }
            return result;
        }

        public async Task<PagedList<Category>> GetListCategories()
        {

            var result = new PagedList<Category>();
            var query1 = _context.Categories.AsQueryable();
            var data = await query1 .ToListAsync();
            result.TotalCount = query1.Count();
            return result;
        }

        public async Task<List<Guid>> GetListIdRefer ( Guid id)
        {
            var query = await _context.Categories.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (query.ParentId == null)
            {
                var childIds = await _context.Categories.Where(i => i.ParentId == id).Select(i => i.Id).ToListAsync();
                return childIds;
            }
            else
            {
                var parentId = await _context.Categories.Where(i => i.Id == query.ParentId).Select(i => i.Id).ToListAsync();
                return parentId;
            }

        }

        public async Task<IEnumerable<Category>> GetAllCategoriesLv1()
        {
            var listId1 = await _context.Categories.Where(i=>i.ParentId == null)
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .ToListAsync();
            return listId1;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesLv2()
        {
            var listId2 = await _context.Categories.Where(i => i.ParentId != null)
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .ToListAsync();
            return listId2;
        }
    }
}
