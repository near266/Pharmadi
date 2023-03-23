using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using System.Diagnostics.SymbolStore;

namespace Module.Catalog.Application.Persistences
{
    public interface IBrandRepository
    {
        Task<int> Add(Brand request);
        Task<int> Update(Brand request);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Brand>> Search(string? keyword);
        Task<PagedList<Brand>> GetAllAdmin(int page, int pageSize);
        Task<int> PinBrand(Guid Id,bool? Pin);
        Task<PagedList<Brand>> IsHaveGroup(int page , int pageSize,int type ,Guid? GroupBrandId);
    }
}
