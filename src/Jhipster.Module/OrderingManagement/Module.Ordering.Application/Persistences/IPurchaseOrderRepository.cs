using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.Utilities;

namespace Module.Ordering.Application.Persistences
{
    public interface IPurchaseOrderRepostitory
    {
        Task<int> Add(PurchaseOrder request);
        Task<int> Update(PurchaseOrder request);
        Task<int> Delete(Guid id);
        Task<PagedList<PurchaseOrder>> GetAllAdmin(int page, int pageSize, int? status);
        Task<PagedList<PurchaseOrder>> GetAllByUser(int page, int pageSize, int? status, Guid userId);
        Task<PurchaseOrder> ViewDetail(Guid id);
    }
}
