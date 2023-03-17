using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Commands.ProductCm
{
    public class ProductDeleteCommand : IRequest<int>
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid Id { get; set; }
    }
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductDeleteCommandHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Id);
        }
    }

}
