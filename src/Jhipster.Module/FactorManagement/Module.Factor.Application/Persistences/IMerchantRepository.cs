using Module.Factor.Domain.Entities;
using Module.Factor.Shared.Utilities;

namespace Module.Factor.Application.Persistences
{
    public interface IMerchantRepository
    {
        Task<int> Add(Merchant request);
        Task<int> Update(Merchant request);
        Task<int> Delete(Guid id);
        Task<PagedList<Merchant>> GetAllAdmin(int page, int pageSize, string? keyword);
        Task<Merchant> ViewDetail(Guid id);
    }
}
