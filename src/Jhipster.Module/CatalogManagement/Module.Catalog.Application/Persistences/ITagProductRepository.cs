using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Persistences
{
    public interface ITagProductRepository
    {
        Task<int> Add(TagProduct request);
        Task<int> Update(TagProduct request);
        Task<int> Delete(Guid productId, Guid tagId);
    }
}
