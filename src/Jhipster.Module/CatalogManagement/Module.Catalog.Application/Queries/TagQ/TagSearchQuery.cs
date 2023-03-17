using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.TagQ
{
    public class TagSearchQuery : IRequest<IEnumerable<Tag>>
    {
        public string? keyword { get; set; }
    }
    public class TagSearchQueryHandler : IRequestHandler<TagSearchQuery, IEnumerable<Tag>>
    {
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;
        public TagSearchQueryHandler(ITagRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Tag>> Handle(TagSearchQuery request, CancellationToken cancellationToken)
        {
            // var obj = _mapper.Map<Tag>(request);
            return await _repo.Search(request.keyword);
        }
    }

}
