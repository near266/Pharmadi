

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ProductViewDetailQuery : IRequest<ProductDetail>
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
    }
    public class ProductViewDetailQueryHandler : IRequestHandler<ProductViewDetailQuery, ProductDetail>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductViewDetailQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ProductDetail> Handle(ProductViewDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewDetail(request.Id,request.UserId);
        }
    }

}
