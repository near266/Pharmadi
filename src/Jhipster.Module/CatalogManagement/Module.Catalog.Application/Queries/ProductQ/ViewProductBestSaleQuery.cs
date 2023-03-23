

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductBestSaleQuery : IRequest<PagedList<Product>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ViewProductBestSaleQueryHandler : IRequestHandler<ViewProductBestSaleQuery, PagedList<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductBestSaleQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Product>> Handle(ViewProductBestSaleQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductBestSale(request.page,request.pageSize);
        }
    }

}
