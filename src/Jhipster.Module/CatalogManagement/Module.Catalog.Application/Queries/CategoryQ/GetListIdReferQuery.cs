using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;


namespace Module.Catalog.Application.Queries.CategoryQ
{
    public class GetListIdReferQuery : IRequest<List<Guid>>
    {
        public Guid id { get; set; }
    }
    public class GetListIdReferQueryHandler : IRequestHandler<GetListIdReferQuery, List<Guid>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        public GetListIdReferQueryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<Guid>> Handle(GetListIdReferQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetListIdRefer(request.id);
        }
    }

}
