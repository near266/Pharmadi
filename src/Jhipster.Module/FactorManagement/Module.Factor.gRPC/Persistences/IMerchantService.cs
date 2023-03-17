using Module.Factor.gRPC.Contracts;
using Module.Factor.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Factor.gRPC.Persistences
{
    [Service]
    public interface  IMerchantService

    {
        [Operation]
        Task<MerchantBaseResponse> Add(MerchantAddRequest request, CallContext context = default);
        Task<MerchantBaseResponse> Update(MerchantUpdateRequest request, CallContext context = default);
        Task<MerchantBaseResponse> Delete(MerchantDeleteRequest request, CallContext context = default);

        Task<PagedListC<MerchantGetAllAdminResponse>> GetAllAdmin(MerchantGetAllAdminRequest request, CallContext context = default);
        Task<MerchantInforResponse> ViewDetail(MerchantViewDetailRequest request, CallContext context = default);

    }
}
