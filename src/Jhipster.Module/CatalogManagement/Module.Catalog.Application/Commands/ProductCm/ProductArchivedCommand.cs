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
    public class ProductArchivedCommand : IRequest<int>
    {
        public List<Guid> Ids { get; set; }
    }
    public class ProductArchivedCommandHandler : IRequestHandler<ProductArchivedCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductArchivedCommandHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(ProductArchivedCommand request, CancellationToken cancellationToken)
        {
            return await _repo.ArchivedProduct(request.Ids);
        }
    }

}
