using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface ITagRepository
    {
        Task<int> Add(Tag request);
        Task<int> Update(Tag request);
        Task<int> Delete(Guid id);
        Task<PagedList<Tag>> GetAllAdmin(int page, int pageSize);
        Task<IEnumerable<Tag>> Search(string? keyword);
    }
}
