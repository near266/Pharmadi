using Module.Catalog.gRPC.Contracts;

using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Ordering.gRPC.Persistences
{
    [Service]
    public interface ICartService
    {
        [Operation]
        Task<CartBaseResponse> Add(CartAddRequest request, CallContext context = default);
        Task<CartBaseResponse> Update(CartUpdateRequest request, CallContext context = default);
        Task<CartBaseResponse> Delete(CartDeleteRequest request, CallContext context = default);
        Task<PagedListC<CartInfor>> GetAllCartByUser(CartGetAllByUserRequest request, CallContext context = default);
    }
}
