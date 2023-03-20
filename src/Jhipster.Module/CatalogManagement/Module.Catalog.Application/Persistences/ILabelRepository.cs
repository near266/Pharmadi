using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface ILabelRepository
    {
        Task<int> Add(Label request);
        Task<int> Update(Label request);
        Task<int> Delete(Guid id);
        Task<IEnumerable<Label>> Search(string? keyword);
        Task<PagedList<Label>> GetAllAdmin(int page, int pageSize);
    }
}
