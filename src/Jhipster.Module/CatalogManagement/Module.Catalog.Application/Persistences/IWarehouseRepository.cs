using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

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
