

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ProductSearchToChooseQuery : IRequest<IEnumerable<Product>>
    {
        public string? keyword { get; set; }
    }
    public class ProductSearchToChooseQueryHandler : IRequestHandler<ProductSearchToChooseQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductSearchToChooseQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Product>> Handle(ProductSearchToChooseQuery request, CancellationToken cancellationToken)
        {
            return await _repo.SearchToChoose(request.keyword);
        }
    }

}
