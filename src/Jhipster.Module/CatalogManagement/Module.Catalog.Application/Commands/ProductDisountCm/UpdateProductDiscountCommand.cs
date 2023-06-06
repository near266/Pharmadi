using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.ProductDisountCm
{
    public class UpdateProductDiscountCommand : IRequest<int>
    {
        public Guid? ProductId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public float Discount { get; set; }
        public string Unit { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }

    }
    public class UpdateProductDiscountCommandHandler : IRequestHandler<UpdateProductDiscountCommand, int>
    {
        private readonly IProductRepository _repository;
        public UpdateProductDiscountCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UpdateProductDiscountCommand request, CancellationToken cancellationToken)
        {
            var map = new ProductDiscount();
            map.ProductId = (Guid)request.ProductId;
            map.Min = request.Min;
            map.Max = request.Max;
            map.Discount = request.Discount;
            map.Unit = request.Unit;
            map.LastModifiedDate = DateTime.Now;
            map.LastModifiedBy = request.CreatedBy;
            return await _repository.UpdateProductDiscount(map);
        }
    }
}
