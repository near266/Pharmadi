using Module.Catalog.gRPC.Contracts;

using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Ordering.gRPC.Persistences
{
    [Service]
    public interface IPurchaseOrderService
    {
        [Operation]
        Task<PurchaseOrderBaseResponse> Add(PurchaseOrderAddRequest request, CallContext context = default);
        Task<PurchaseOrderBaseResponse> Update(PurchaseOrderUpdateRequest request, CallContext context = default);
        Task<PurchaseOrderBaseResponse> Delete(PurchaseOrderDeleteRequest request, CallContext context = default);
        Task<PagedListC<PurchaseOrderInforAdmin>> GetAllPurchaseOrderByAdmin(PurchaseOrderGetAllAdminRequest request, CallContext context = default);
        Task<PagedListC<PurchaseOrderInforUser>> GetAllPurchaseOrderByUser(PurchaseOrderGetAllUserRequest request, CallContext context = default);
        Task<PurchaseOrderInforDetail> ViewDetailPurchaseOrder(PurchaseOrderViewDetailRequest request, CallContext context = default);
    }
}
