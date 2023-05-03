using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.BrandQ
{
    public class BrandSearchQuery : IRequest<IEnumerable<BrandDTO>>
    {
        public string? keyword { get; set;}
    }
    public class BrandSearchQueryHandler : IRequestHandler<BrandSearchQuery, IEnumerable<BrandDTO>>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandSearchQueryHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<BrandDTO>> Handle(BrandSearchQuery request, CancellationToken cancellationToken)
        {
            return await _repo.Search(request.keyword);
        }
    }

}
