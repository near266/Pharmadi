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
    public class ViewProductSimilarQuery : IRequest<IEnumerable<Product>>
    {
        public Guid Id { get; set; }
    }
    public class ViewProductSimilarQueryHandler : IRequestHandler<ViewProductSimilarQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductSimilarQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Product>> Handle(ViewProductSimilarQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewListProductSimilarCategory(request.Id);
        }
    }
}
