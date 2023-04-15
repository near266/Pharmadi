using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Persistences
{
    public interface ICategoryProductRepository
    {
        Task<int> Add(CategoryProduct request);
        Task<int> Update(CategoryProduct request);
        Task<int> Delete(Guid productid, Guid categoryId);
    }
}
