

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductPromotionQuery : IRequest<PagedList<Product>>
    {
        public string keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ViewProductPromotionQueryHandler : IRequestHandler<ViewProductPromotionQuery, PagedList<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductPromotionQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Product>> Handle(ViewProductPromotionQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductPromotion(request.keyword,request.page, request.pageSize);
        }
    }

}
