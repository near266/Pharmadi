using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.GroupBrandQ
{
    public class GroupBrandGetAllAdminQuery : IRequest<PagedList<GroupBrand>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class GroupBrandGetAllQueryHandler : IRequestHandler<GroupBrandGetAllAdminQuery, PagedList<GroupBrand>>
    {
        private readonly IGroupBrandRepository _repo;
        private readonly IMapper _mapper;
        public GroupBrandGetAllQueryHandler(IGroupBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<GroupBrand>> Handle(GroupBrandGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize);
        }
    }

}
