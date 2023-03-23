using Module.Factor.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Factor.Application.Persistences
{
    public interface IMerchantRepository
    {
        Task<int> Add(Merchant request);
        Task<int> Update(Merchant request);
        Task<int> Delete(Guid id);
        Task<PagedList<Merchant>> GetAllAdmin(int page, int pageSize, string? keyword);
        Task<Merchant> ViewDetail(Guid id);
        Task<IEnumerable<Merchant>> SearchToChoose(string? keyword);
    }
}
