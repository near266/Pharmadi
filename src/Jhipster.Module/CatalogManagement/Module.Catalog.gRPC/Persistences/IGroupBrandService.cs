using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Catalog.gRPC.Persistences
{
    [Service]
    public interface IGroupBrandService
    {
        [Operation]
        Task<GroupBrandBaseResponse> Add(GroupBrandAddRequest request, CallContext context = default);
        Task<GroupBrandBaseResponse> Update(GroupBrandUpdateRequest request, CallContext context = default);
        Task<GroupBrandBaseResponse> Delete(GroupBrandDeleteRequest request, CallContext context = default);
        Task<IEnumerable<GroupBrandSearchResponse>> Search(GroupBrandSearchRequest request, CallContext context = default);
        Task<PagedListC<GroupBrandGetAllAdminResponse>> GetAllAdmin(GroupBrandGetAllAdminRequest request, CallContext context = default);
    }
}
