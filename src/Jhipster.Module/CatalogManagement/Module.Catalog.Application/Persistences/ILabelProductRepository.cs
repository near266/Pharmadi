using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Persistences
{
    public interface ILabelProductRepository
    {
        Task<int> Add(LabelProduct request);
        Task<int> Update(LabelProduct request);
        Task<int> Delete(Guid productId, Guid labelId);
    }
}
