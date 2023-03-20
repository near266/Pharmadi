using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.GroupBrandQ
{
    public class GroupBrandSearchQuery : IRequest<IEnumerable<GroupBrand>>
    {
        public string? keyword { get; set; }
    }
    public class GroupBrandSearchQueryHandler : IRequestHandler<GroupBrandSearchQuery, IEnumerable<GroupBrand>>
    {
        private readonly IGroupBrandRepository _repo;
        private readonly IMapper _mapper;
        public GroupBrandSearchQueryHandler(IGroupBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<GroupBrand>> Handle(GroupBrandSearchQuery request, CancellationToken cancellationToken)
        {
            return await _repo.Search(request.keyword);
        }
    }

}
