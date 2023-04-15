using Jhipster.Service.Utilities;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Persistences
{
    public interface IOrderStatusRepostitory
    {
        Task<int> Add(OrderStatus request);
        Task<IEnumerable<OrderStatus>> GetAll(Guid Id);
    }
}
