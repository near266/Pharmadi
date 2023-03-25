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
    public  class GetListProductSimilarCategoryByBrandIdQuery : IRequest<IEnumerable<ProductSearchDTO>>
    {
        public Guid brandId { get; set; }
        public Guid UserId { get; set; }
    }
    public class GetListProductSimilarCategoryByBrandIdQueryHandler : IRequestHandler<GetListProductSimilarCategoryByBrandIdQuery, IEnumerable<ProductSearchDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public GetListProductSimilarCategoryByBrandIdQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<ProductSearchDTO>> Handle(GetListProductSimilarCategoryByBrandIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetListProductSimilarCategoryByBrandId(request.brandId,request.UserId);
        }
    }
}
