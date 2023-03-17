using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Catalog.Application.Commands.ProductCm
{
    public class ProductUpdateCommand: IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string SKU { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string ProductName { get; set; }
        public string? Function { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Description { get; set; }
        public string? UnitName { get; set; }
        public Guid BrandId { get; set; }
        public int Status { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
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
