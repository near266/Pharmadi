using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface IBrandRepository
    {
        Task<int> Add(Brand request);
        Task<int> Update(Brand request);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Brand>> Search(string? keyword);
        Task<PagedList<Brand>> GetAllAdmin(int page, int pageSize);
    }
}
