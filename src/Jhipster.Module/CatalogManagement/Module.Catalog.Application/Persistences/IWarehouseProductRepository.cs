using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Persistences
{
    public interface IWarehouseProductRepository
    {
        Task<int> Add(WarehouseProduct request);
        Task<int> Update(WarehouseProduct request);
        Task<int> Delete(Guid id);
        Task<int> CountProduct(Guid productId);
        Task<IEnumerable<WarehouseProduct>> ListLotDateByProduct(Guid id);
    }
}
