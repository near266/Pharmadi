using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.Utilities;

namespace Module.Catalog.Application.Queries.TagQ
{
    public class TagGetAllAdminQuery : IRequest<PagedList<Tag>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class TagGetAllAdminQueryHandler : IRequestHandler<TagGetAllAdminQuery, PagedList<Tag>>
    {
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;
        public TagGetAllAdminQueryHandler(ITagRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Tag>> Handle(TagGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize);
        }
    }

}
