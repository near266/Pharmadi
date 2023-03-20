using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface IGroupBrandRepository
    {
        Task<int> Add(GroupBrand request);
        Task<int> Update(GroupBrand request);
        Task<int> Delete(Guid id);
        Task<PagedList<GroupBrand>> GetAllAdmin(int page, int pageSize);
        Task<IEnumerable<GroupBrand>> Search(string? keyword);
    }
}
