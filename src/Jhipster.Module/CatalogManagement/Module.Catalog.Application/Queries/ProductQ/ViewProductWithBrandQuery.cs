using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductWithBrandQuery:IRequest<IEnumerable<Product>>
    {
        public Guid Id { get; set; } 
    }
    public class ViewProductWithBrandQueryHandler : IRequestHandler<ViewProductWithBrandQuery,IEnumerable< Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductWithBrandQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Product>> Handle(ViewProductWithBrandQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewListProductWithBrand(request.Id);
        }
    }
}
