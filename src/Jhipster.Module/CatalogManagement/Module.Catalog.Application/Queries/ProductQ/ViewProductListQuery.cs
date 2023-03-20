using AutoMapper;
using Jhipster.Service.Utilities;
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
        public  class ViewProductListQuery : IRequest<IEnumerable<Product>>
          {
                 public Guid Id { get; set; }
          }
    
        public class ViewProductListQueryHandler : IRequestHandler<ViewProductListQuery, IEnumerable<Product>>
        {
            private readonly IProductRepository _repo;
            private readonly IMapper _mapper;
            public ViewProductListQueryHandler(IProductRepository repo, IMapper mapper)
            {
                _repo = repo ?? throw new ArgumentNullException(nameof(repo));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public async Task<IEnumerable<Product>> Handle(ViewProductListQuery request, CancellationToken cancellationToken)
            {
                var rq = _mapper.Map<Product>(request);
                return await _repo.ViewProductList(rq.Id);
            }
        }
    
}
