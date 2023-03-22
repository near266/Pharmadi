﻿using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface IProductRepository
    {
        Task<int> Add(Product request);
        Task<int> Update(Product request);
        Task<int> Delete(Guid id);
        Task<PagedList<Product>> GetAllAdmin(int page, int pageSize, string? SKU, string? ProductName, int? status);
        Task<Product> ViewDetail(Guid Id);
        Task<PagedList<Product>> ViewProductForU( string? keyword,int page, int pageSize);
        Task<PagedList<Product>> ViewProductBestSale(int page, int pageSize);
        Task<PagedList<Product>> ViewProductNew(int page, int pageSize);
        Task<PagedList<Product>> ViewProductPromotion(string? keyword,int page, int pageSize);
        Task<PagedList<Product>> SearchProduct(string? keyword, List<Guid?>? categoryIds, List<Guid?>? brandIds, List<Guid?>? tagIds, int page, int pageSize);
        Task<int> UpdataStatusProduct(Guid id ,int status);
        Task<IEnumerable<Product>> ViewListProductWithBrand(Guid Id);
        Task<PagedList<Product>> ViewListProductSimilarCategory(Guid Id,int page, int pageSize);
        Task<List<List<string>>> FakeData();
    }
}
