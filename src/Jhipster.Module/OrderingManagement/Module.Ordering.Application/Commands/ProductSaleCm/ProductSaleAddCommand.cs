using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Commands.ProductSaleCm
{
    public class ProductSaleAddCommand : IRequest<int>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? dateTime { get; set; }
    }
    public class ProductSaleAddCommandHandler : IRequestHandler<ProductSaleAddCommand, int>
    {
        private readonly IProductSaleRepostitory _repo;
        private readonly IMapper _mapper;
        public ProductSaleAddCommandHandler(IProductSaleRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(ProductSaleAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<ProductSale>(request);
            return await _repo.Add(obj);
        }
    }

}
