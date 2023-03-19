using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.Utilities;

namespace Module.Ordering.Application.Persistences
{
    public interface IOrderItemRepostitory
    {
        Task<int> Add(OrderItem request);
        Task<int> Update(OrderItem request);
        Task<int> Delete(Guid id);
        Task<PagedList<OrderItem>> GetAllItemByOrder(int page, int pageSizem, Guid OrderId);
    }
}
