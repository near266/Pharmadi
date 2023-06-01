using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Persistences
{
    public interface IProductRepository
    {
        Task<int> Add(Product request);
        Task<int> Update(Product request);
        Task<int> Delete(Guid id);
        Task<PagedList<Product>> GetAllAdmin(int page, int pageSize, string? SKU, string? ProductName, int? status);
        Task<Product> ViewDetail(Guid Id);
        Task<PagedList<ProductSearchDTO>> ViewProductForU(string? keyword, int page, int pageSize, Guid? userId);
        Task<PagedList<SaleProductDTO>> ViewProductBestSale(int page, int pageSize, Guid? userId);
        Task<PagedList<NewProductDTO>> ViewProductNew(int page, int pageSize, Guid? userId);
        Task<PagedList<ViewProductPromotionDTO>> ViewProductPromotion(string? keyword, int page, int pageSize, Guid? userId);
        Task<PagedList<SearchMcProductDTO>> SearchProduct(string? keyword, List<Guid> categoryIds, List<Guid> cateLevel2Ids, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize, Guid? userId);
        Task<int> UpdataStatusProduct(Guid id, int status);
        Task<IEnumerable<ProductSearchDTO>> ViewListProductWithBrand(Guid Id, Guid? userId);
        Task<PagedList<ProductSearchDTO>> ViewListProductSimilarCategory(Guid Id, Guid? userId);
        Task<List<List<string>>> FakeData();
        Task<IEnumerable<SearchToChooseDTO>> SearchToChoose(string? keyword);
        Task<PagedList<SearchProductBrandId>> GetListProductSimilarCategoryByBrandId(int page, int pageSize, Guid brandId, Guid? userId);
        Task<int> ArchivedProduct(List<Guid> ids);
        Task<PagedList<ViewProductForeignDTO>> ViewProductForeign(string? keyword, int page, int pageSize, Guid? userId);
        Task<List<Guid>> GetPorductIdbyBrandId(Guid Brandid);
        Task<IEnumerable<ProductClassificationByCountryDTO>> ProductClassificationByCountry(int page, int pageSize, int Type);
        Task<int> UpdateProductDiscount(ProductDiscount rq);
        Task<int> DeleteDiscountProduct(Guid id);
        Task<List<ProductDiscount>> ViewDiscountByUserId(Guid id);
        Task<int> AddProductDiscount(ProductDiscount rq);
    }
}
