
using Module.Ordering.gRPC.Contracts.PagedListC;
using Module.Ordering.gRPC.Contracts;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Ordering.gRPC.Persistences
{
    [Service]
    public interface IOrderItemService
    {
        [Operation]
        Task<OrderItemBaseResponse> Add(OrderItemAddRequest request, CallContext context = default);
        Task<OrderItemBaseResponse> Update(OrderItemUpdateRequest request, CallContext context = default);
        Task<OrderItemBaseResponse> Delete(OrderItemDeleteRequest request, CallContext context = default);
        Task<PagedListC<OrderItemInfor>> ItemsGetAllByOrder(ItemGetAllByOrderRequest request, CallContext context = default);
    }
}
