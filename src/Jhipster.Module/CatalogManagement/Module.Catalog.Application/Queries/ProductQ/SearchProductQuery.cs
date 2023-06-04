

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class SearchProductQuery : IRequest<PagedList<SearchMcProductDTO>>
    {
        public string? keyword { get; set; }
        public List<Guid> categoryIds { get; set; }
        public List<Guid> cateLevel2Ids { get; set; }
        public List<Guid?>? brandIds { get; set; }
        public List<Guid?>? tagIds { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid? userId { get; set; }
    }
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedList<SearchMcProductDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public SearchProductQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<SearchMcProductDTO>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            return await _repo.SearchProduct(request.keyword, request.categoryIds,request.cateLevel2Ids, request.brandIds, request.tagIds,request.page, request.pageSize, request.userId);
        }
    }

}
