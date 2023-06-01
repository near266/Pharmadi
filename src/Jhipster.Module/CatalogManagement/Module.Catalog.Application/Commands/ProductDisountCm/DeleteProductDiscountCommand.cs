using MediatR;
using Module.Catalog.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.ProductDisountCm
{
    public class DeleteProductDiscountCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProductDiscountCommandHandler : IRequestHandler<DeleteProductDiscountCommand, int>
    {
        private readonly IProductRepository _repository;
        public DeleteProductDiscountCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteProductDiscountCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteDiscountProduct(request.Id);
        }
    }
}
