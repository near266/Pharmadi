using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.ProductDiscountQ
{
    public class ViewDiscountByUserIdQuery:IRequest<List<ProductDiscount>>
    {
        public Guid Id {  get; set; }   
    }
    public class ViewDiscountByUserIdQueryHandler : IRequestHandler<ViewDiscountByUserIdQuery, List<ProductDiscount>>
    {
        private readonly IProductRepository _repository;
        public ViewDiscountByUserIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDiscount>> Handle(ViewDiscountByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ViewDiscountByUserId(request.Id);
        }
    }
}
