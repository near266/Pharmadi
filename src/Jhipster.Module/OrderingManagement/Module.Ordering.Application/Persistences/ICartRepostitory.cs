using Jhipster.Service.Utilities;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Persistences
{
    public interface ICartRepostitory
    {
        Task<int> Add(Cart request);
        Task<int> Update(Cart request);
        Task<int> Delete(List<Guid> ids);
        Task<ViewCartDTO> GetAllByUser(int page, int pageSize, Guid userId, int? check);
        Task<List<Cart>> GetCartChoice(Guid userId);
        Task<CartResultDTO> CartResultSum(Guid userId);
        Task<List<ViewQuickOrder>> ViewQuick(Guid userId, int Type, string keyword);
    }
}
