using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Catalog.gRPC.Persistences
{
    [Service]
    public interface ILabelService
    {
        [Operation]
        Task<LabelBaseResponse> Add(LabelAddRequest request, CallContext context = default);
        Task<LabelBaseResponse> Update(LabelUpdateRequest request, CallContext context = default);
        Task<LabelBaseResponse> Delete(LabelDeleteRequest request, CallContext context = default);
        Task<IEnumerable<LabelSearchResponse>> Search(LabelSearchRequest request, CallContext context = default);
        Task<PagedListC<LabelGetAllAdminResponse>> GetAllAdmin(LabelGetAllAdminRequest request, CallContext context = default);
    }
}
