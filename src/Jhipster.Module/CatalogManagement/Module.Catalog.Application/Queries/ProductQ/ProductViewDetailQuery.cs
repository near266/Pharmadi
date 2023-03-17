

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ProductViewDetailQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
    }
    public class ProductViewDetailQueryHandler : IRequestHandler<ProductViewDetailQuery, Product>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductViewDetailQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Product> Handle(ProductViewDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewDetail(request.Id);
        }
    }

}
