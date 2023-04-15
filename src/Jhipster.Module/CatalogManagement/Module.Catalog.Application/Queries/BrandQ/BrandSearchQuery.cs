using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;



namespace Module.Catalog.Application.Queries.BrandQ
{
    public class BrandSearchQuery : IRequest<IEnumerable<Brand>>
    {
        public string? keyword { get; set;}
    }
    public class BrandSearchQueryHandler : IRequestHandler<BrandSearchQuery, IEnumerable<Brand>>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandSearchQueryHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Brand>> Handle(BrandSearchQuery request, CancellationToken cancellationToken)
        {
            return await _repo.Search(request.keyword);
        }
    }

}
