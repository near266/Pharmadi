﻿

using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ProductGetAllAdminQuery : IRequest<PagedList<Product>>
    {
        public string? SKU { get; set; }    
        public string? ProductName { get; set; }
        public int? status { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BrandName { get; set; }
        public string? CatalogName { get;set; }
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
            return await _repo.GetAllAdmin(request.page, request.pageSize,request.SKU,request.ProductName,request.status,request.BrandName,request.CatalogName,request.StartDate,request.EndDate);
        }
    }

}
