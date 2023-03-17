using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface IWarehouseRepository
    {
        Task<int> Add(Warehouse request);
        Task<int> Update(Warehouse request);
        Task<int> Delete(Guid id);
        Task<PagedList<Warehouse>> GetAllAdmin(int page, int pageSize);
    }
}
