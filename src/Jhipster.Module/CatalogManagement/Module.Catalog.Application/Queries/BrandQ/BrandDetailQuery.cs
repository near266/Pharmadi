using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.BrandQ
{
    public class BrandDetailQuery :IRequest<DetailBrand>
    {
      public Guid Id { get; set; }
    }   
    public class BrandDetailQueryHandler : IRequestHandler<BrandDetailQuery,DetailBrand>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandDetailQueryHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<DetailBrand >Handle(BrandDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.BrandDetail(request.Id);
        }
    }
}
