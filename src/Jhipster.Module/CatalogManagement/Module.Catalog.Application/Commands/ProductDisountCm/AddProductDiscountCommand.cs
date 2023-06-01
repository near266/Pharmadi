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
    public class AddProductDiscountCommand : IRequest<int>
    {
        public Guid ProductId { get; set; }
        public int Range { get; set; }
        public float Discount { get; set; }
        public string Unit { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }

    }
    public class AddProductDiscountCommandHandler : IRequestHandler<AddProductDiscountCommand, int>
    {
        private readonly IProductRepository _repository;
        public AddProductDiscountCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddProductDiscountCommand request, CancellationToken cancellationToken)
        {
            var map = new ProductDiscount();
            map.ProductId = request.ProductId;
            map.Range = request.Range;
            map.Discount = request.Discount;
            map.Unit = request.Unit;
            map.CreatedDate = DateTime.Now;
            map.CreatedBy = request.CreatedBy;
            return await _repository.AddProductDiscount(map);
        }
    }
}
