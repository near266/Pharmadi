using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Module.Catalog.gRPC.Persistences
{
    [Service]
    public interface IProductService
    {
        [Operation]
        Task<ProductBaseResponse> Add(ProductAddRequest request, CallContext context = default);
        Task<ProductBaseResponse> Update(ProductUpdateRequest request, CallContext context = default);
        Task<ProductBaseResponse> Delete(ProductDeleteRequest request, CallContext context = default);
        Task<PagedListC<ProductInforSearchResponse>> Search(ProductSearchRequest request, CallContext context = default);
        Task<PagedListC<ProductGetAllAdminResponse>> GetAllAdmin(ProductGetAllAdminRequest request, CallContext context = default);
        Task<ProductViewDetailResponse> ViewDetail(ProductViewDetailRequest request, CallContext context = default);
        Task<IEnumerable<ProductInforSearchResponse>> ViewProductForU(ProductSearchListRequest request, CallContext context = default);
        Task<IEnumerable<ProductInforSearchResponse>> ViewProductBestSale(ProductSearchListRequest request, CallContext context = default);
        Task<IEnumerable<ProductInforSearchResponse>> ViewProductNew(ProductSearchListRequest request, CallContext context = default);
        Task<IEnumerable<ProductInforSearchResponse>> ViewProductPromotion(ProductSearchListRequest request, CallContext context = default);

    }
}
