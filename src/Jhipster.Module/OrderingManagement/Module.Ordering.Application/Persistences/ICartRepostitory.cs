using Jhipster.Service.Utilities;
using Module.Ordering.Domain.Entities;



namespace Module.Ordering.Application.Persistences
{
    public interface ICartRepostitory
    {
        Task<int> Add(Cart request);
        Task<int> Update(Cart request);
        Task<int> Delete(Guid id);
        Task<PagedList<Cart>> GetAllByUser(int page, int pageSize, Guid userId);
    }
}
