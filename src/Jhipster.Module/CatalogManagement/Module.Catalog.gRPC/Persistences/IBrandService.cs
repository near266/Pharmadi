using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Catalog.gRPC.Persistences
{
    [Service]
    public interface IBrandService
    {
        [Operation]
        Task<BrandBaseResponse> Add(BrandAddRequest request, CallContext context = default);
        Task<BrandBaseResponse> Update(BrandUpdateRequest request, CallContext context = default);
        Task<BrandBaseResponse> Delete(BrandDeleteRequest request, CallContext context = default);
        Task<IEnumerable<BrandSearchResponse>> Search(BrandSearchRequest request, CallContext context = default);
        Task<PagedListC<BrandGetAllAdminResponse>> GetAllAdmin(BrandGetAllAdminRequest request, CallContext context = default);
    }
}
