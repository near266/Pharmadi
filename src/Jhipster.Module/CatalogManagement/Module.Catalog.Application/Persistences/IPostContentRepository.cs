using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Persistences
{
    public interface IPostContentRepository
    {
        Task<int> Add(PostContent request);
        Task<int> Update(PostContent request);
        Task<int> Delete(Guid id);
        Task<PagedList<PostContent>> GetAll(int page, int pageSize);
        Task<PostContent> ViewDetail(Guid id);
    }
}
