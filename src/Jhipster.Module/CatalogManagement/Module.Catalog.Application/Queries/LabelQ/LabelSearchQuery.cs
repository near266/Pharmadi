using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.LabelQ
{
    public class LabelSearchQuery : IRequest<IEnumerable<Label>>
    {
        public string? keyword { get; set; }
    }
    public class LabelSearchQueryHandler : IRequestHandler<LabelSearchQuery, IEnumerable<Label>>
    {
        private readonly ILabelRepository _repo;
        private readonly IMapper _mapper;
        public LabelSearchQueryHandler(ILabelRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Label>> Handle(LabelSearchQuery request, CancellationToken cancellationToken)
        {
            // var obj = _mapper.Map<Label>(request);
            return await _repo.Search(request.keyword);
        }
    }

}
