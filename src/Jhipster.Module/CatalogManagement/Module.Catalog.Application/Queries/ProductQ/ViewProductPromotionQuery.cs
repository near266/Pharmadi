

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductPromotionQuery : IRequest<PagedList<ProductSearchDTO>>
    {
        public string? keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid? userId { get; set; }
    }
    public class ViewProductPromotionQueryHandler : IRequestHandler<ViewProductPromotionQuery, PagedList<ProductSearchDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductPromotionQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<ProductSearchDTO>> Handle(ViewProductPromotionQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewProductPromotion(request.keyword,request.page, request.pageSize, request.userId);
        }
    }

}
