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
    public class BrandRepresentativeQuery :IRequest<PagedList<BrandDTO>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class BrandRepresentativeQueryHandler : IRequestHandler<BrandRepresentativeQuery,PagedList<BrandDTO>>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandRepresentativeQueryHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<BrandDTO> >Handle(BrandRepresentativeQuery request, CancellationToken cancellationToken)
        {
            return await _repo.BrandRepresentative(request.page,request.pageSize);
        }
    }
}
