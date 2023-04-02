using Jhipster.Service.Utilities;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Persistences
{
    public interface IProductSaleRepostitory
    {
        Task<int> Add(ProductSale request);
       
    }
}
