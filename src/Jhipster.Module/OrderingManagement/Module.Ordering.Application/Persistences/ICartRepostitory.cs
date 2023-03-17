using Module.Ordering.Domain.Entities;

namespace Module.Ordering.Application.Persistences
{
    public interface ICartRepostitory
    {
        Task<int> Add(Cart request);
        Task<int> Update(Cart request);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Cart>> GetAll();
    }
}
