﻿

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;
using Module.Catalog.Shared.DTOs;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class SearchProductQuery : IRequest<PagedList<ProductSearchDTO>>
    {
        public string? keyword { get; set; }
        public List<Guid?>? categoryIds { get; set; }
        public List<Guid?>? brandIds { get; set; }
        public List<Guid?>? tagIds { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid? userId { get; set; }
    }
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedList<ProductSearchDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public SearchProductQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<ProductSearchDTO>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            return await _repo.SearchProduct(request.keyword, request.categoryIds, request.brandIds, request.tagIds,request.page, request.pageSize, request.userId);
        }
    }

}
