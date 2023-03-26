using Jhipster.Service.Utilities;
using Module.Ordering.Domain.Entities;



namespace Module.Ordering.Application.Persistences
{
    public interface IOrderItemRepostitory
    {
        Task<int> Add(OrderItem request);
        Task<int> Update(OrderItem request);
        Task<int> Delete(List<Guid> ids);
        Task<PagedList<OrderItem>> GetAllItemByOrder(int page, int pageSizem, Guid OrderId);
    }
}
