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
    public class ViewProductSimilarQuery : IRequest<PagedList<Product>>
    {
        public Guid Id { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ViewProductSimilarQueryHandler : IRequestHandler<ViewProductSimilarQuery, PagedList<Product>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductSimilarQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Product>> Handle(ViewProductSimilarQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewListProductSimilarCategory(request.Id,request.page,request.pageSize);
        }
    }
}
