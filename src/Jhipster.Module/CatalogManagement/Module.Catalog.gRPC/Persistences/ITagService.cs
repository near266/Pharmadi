using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Catalog.gRPC.Persistences
{
    [Service]
    public interface ITagService
    {
        [Operation]
        Task<TagBaseResponse> Add(TagAddRequest request, CallContext context = default);
        Task<TagBaseResponse> Update(TagUpdateRequest request, CallContext context = default);
        Task<TagBaseResponse> Delete(TagDeleteRequest request, CallContext context = default);
        Task<IEnumerable<TagSearchResponse>> Search(TagSearchRequest request, CallContext context = default);
        Task<PagedListC<TagGetAllAdminResponse>> GetAllAdmin(TagGetAllAdminRequest request, CallContext context = default);
    }
}
