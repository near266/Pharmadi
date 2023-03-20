﻿using AutoMapper;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Jhipster.Service.Utilities;

namespace Module.Catalog.Application.Queries.BrandQ
{
    public class BrandGetAllAdminQuery : IRequest<PagedList<Brand>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class BrandGetAllQueryHandler : IRequestHandler<BrandGetAllAdminQuery, PagedList<Brand>>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandGetAllQueryHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Brand>> Handle(BrandGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize);
        }
    }

}
