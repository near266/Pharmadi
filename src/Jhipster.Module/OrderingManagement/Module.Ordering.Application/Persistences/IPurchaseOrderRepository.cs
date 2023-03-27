using Module.Ordering.Domain.Entities;
using Jhipster.Service.Utilities;
using Module.Ordering.Application.DTO;

namespace Module.Ordering.Application.Persistences
{
    public interface IPurchaseOrderRepostitory
    {
        #region PurchaseOrder
        Task<int> Add(PurchaseOrder request);
        Task<int> Update(PurchaseOrder request);
        Task<int> Delete(Guid id);
        Task<PagedList<PurchaseOrder>> GetAllAdmin(int page, int pageSize, int? status, DateTime? fromDate, DateTime? toDate, string? codekey, string? customerkey);
        Task<PagedList<PurchaseOrder>> GetAllByUser(int page, int pageSize, int? status, Guid userId);
        Task<PurchaseOrder> ViewDetail(Guid id);
        Task<int> UpdateStatus(Guid Id, int Status);
        Task<PagedList<HistoryOrderDTO>> transactionHistory(Guid id, int? type, int? Status, string? OrderCode, string? productKey, DateTime? fromDate, DateTime? toDate, int page, int pageSize);
        #endregion
        #region HistoryOrder
        Task<int> AddHistoryOrder(HistoryOrder order);
        Task<int> AddHistoryOrderByPurcharseId(Guid Id);
        #endregion
    }
}
