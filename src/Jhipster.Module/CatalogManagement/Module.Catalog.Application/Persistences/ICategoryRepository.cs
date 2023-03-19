using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface ICategoryRepository
    {
        Task<int> Add(Category request);
        Task<int> Update(Category request);
        Task<int> Delete(Guid id);
        Task<PagedList<Category>> GetAllAdmin(int page, int pageSize);
        Task<PagedList<Category>> GetListCategories();
        Task<IEnumerable<Category>> Search(string? keyword);
    }
}
