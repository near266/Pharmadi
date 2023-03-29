using Jhipster.Service.Utilities;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Persistences
{
    public interface IOrderItemRepostitory
    {
        Task<int> Add(OrderItem request);
        Task<int> Update(OrderItem request);
        Task<int> Delete(List<Guid> ids);
        Task<PagedList<OrderItemResponse>> GetAllItemByOrder(Guid OrderId);
        Task<PagedList<OrderItem>> GetAllItemByOrderAdmin(Guid OrderId);
    }
}
