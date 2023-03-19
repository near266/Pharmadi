using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Catalog.gRPC.Persistences
{
    [Service]
    public interface ICategoryService
    {
        [Operation]
        Task<CategoryBaseResponse> Add(CategoryAddRequest request, CallContext context = default);
        Task<CategoryBaseResponse> Update(CategoryUpdateRequest request, CallContext context = default);
        Task<CategoryBaseResponse> Delete(CategoryDeleteRequest request, CallContext context = default);
        Task<IEnumerable<CategorySearchResponse>> Search(CategorySearchRequest request, CallContext context = default);
        Task<PagedListC<CategoryGetAllAdminResponse>> GetAllAdmin(CategoryGetAllAdminRequest request, CallContext context = default);
        Task<PagedListC<CategoryGetAllAdminResponse>> GetListCatalory(GetListCataloryRequest request, CallContext context = default);

    }
}
