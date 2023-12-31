﻿using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Module.Catalog.Application.Commands.ProductCm
{
    public class ProductAddCommand : IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string? UserObject { get; set; }
        public decimal? SuggestPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Description { get; set; }
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public int Status { get; set; }
        public Guid? PostContentId { get; set; }
        public bool? HideProduct { get; set; }
        public List<string>? Image { get; set; }
        public string? Industry { get; set; }
        public string? Warning { get; set; }
        public string? Preserve { get; set; }
        public string? Dosage { get; set; }
        public string? DosageForms { get; set; }
        public string? Country { get; set; }
        public string? Ingredient { get; set; }
        public string? Usage { get; set; }
        public bool? Archived { get; set; }
        public string? Specification { get; set; }
        public int? Number { get; set; }
        public int? NewProduct { get; set; }
        public int? ImportedProducts { get; set; }
        public int? sellingProducts { get; set; }
        public string? ShortName { get; set; }
        public string? BannerProduct1 { get; set; }
        public string? BannerProduct2 { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        public bool? CanOrder { get; set; }
        public string? Different { get; set; }
        public string? ClinicalResearch { get; set; }
    }
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductAddCommandHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Product>(request);
            return await _repo.Add(obj);
        }
    }

}
