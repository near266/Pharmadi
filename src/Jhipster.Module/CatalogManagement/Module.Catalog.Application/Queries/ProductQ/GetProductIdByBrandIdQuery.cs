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
    public class GetProductIdByBrandIdQuery:IRequest<List<Guid>>
    {
        public Guid BrandId { get; set; }   
    }
    public class GetProductIdByBrandIdQueryHandler : IRequestHandler<GetProductIdByBrandIdQuery, List<Guid>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public GetProductIdByBrandIdQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<Guid>> Handle(GetProductIdByBrandIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetPorductIdbyBrandId(request.BrandId);
        }
    }
}
