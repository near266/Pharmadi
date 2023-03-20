

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ProductGetAllAdminQuery : IRequest<PagedList<Product>>
    {
        public string? keyword { get; set; }    
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ProductGetAllProductQueryHandler : IRequestHandler<ProductGetAllAdminQuery, PagedList<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductGetAllProductQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Product>> Handle(ProductGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize,request.keyword);
        }
    }

}
