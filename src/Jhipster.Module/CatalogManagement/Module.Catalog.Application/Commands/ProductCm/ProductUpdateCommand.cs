using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Module.Catalog.Application.Commands.ProductCm
{
    public class ProductUpdateCommand: IRequest<int>
    {
        public Guid Id { get; set; }
        public string? SKU { get; set; }
        public string? ProductName { get; set; }
        public string? Function { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Description { get; set; }
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public int? Status { get; set; }
        public Guid? PostContentId { get; set; }
        public bool? HideProduct { get; set; }
        public List<string>? Image { get; set; }
        public string? Industry { get; set; }
        public string? Effect { get; set; }
        public string? Preserve { get; set; }
        public bool? Archived { get; set; }
        public string? Dosage { get; set; }
        public string? DosageForms { get; set; }
        public string? Country { get; set; }
        public string? Ingredient { get; set; }
        public string? Usage { get; set; }
        public string? Specification { get; set; }
        public int? Number { get; set; }
        [JsonIgnore]
        public Guid? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        public bool? CanOrder { get; set; }
        public int? NewProduct { get; set; }
        public int? ImportedProducts { get; set; }
        public int? sellingProducts { get; set; }
    }
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductUpdateCommandHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<Product>(request);
            return await _repo.Update(obj);
        }
    }

}
