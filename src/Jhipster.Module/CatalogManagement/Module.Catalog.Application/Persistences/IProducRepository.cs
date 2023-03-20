using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface IProductRepository
    {
        Task<int> Add(Product request);
        Task<int> Update(Product request);
        Task<int> Delete(Guid id);
        Task<PagedList<Product>> GetAllAdmin(int page, int pageSize, string? keyword);
        Task<Product> ViewDetail(Guid Id);
        Task<IEnumerable<Product>> ViewProductForU(int page, int pageSize);
        Task<IEnumerable<Product>> ViewProductBestSale(int page, int pageSize);
        Task<IEnumerable<Product>> ViewProductNew(int page, int pageSize);
        Task<IEnumerable<Product>> ViewProductPromotion(int page, int pageSize);
        Task<PagedList<Product>> SearchProduct(string? keyword, List<Guid?>? categoryIds, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize);
    }
}
