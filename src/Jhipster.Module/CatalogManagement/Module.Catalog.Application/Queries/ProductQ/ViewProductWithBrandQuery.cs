using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ViewProductWithBrandQuery:IRequest<IEnumerable<ProductSearchDTO>>
    {
        public Guid Id { get; set; } 
        public Guid? userId { get; set; }
    }
    public class ViewProductWithBrandQueryHandler : IRequestHandler<ViewProductWithBrandQuery,IEnumerable<ProductSearchDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ViewProductWithBrandQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<ProductSearchDTO>> Handle(ViewProductWithBrandQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewListProductWithBrand(request.Id,request.userId);
        }
    }
}
