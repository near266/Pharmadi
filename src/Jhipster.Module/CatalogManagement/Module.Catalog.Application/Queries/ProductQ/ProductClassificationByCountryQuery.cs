﻿using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.ProductQ
{
    public class ProductClassificationByCountryQuery :IRequest<IEnumerable<ProductClassificationByCountryDTO>>
    {
        public int Type { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class ProductClassificationByCountryQueryHandler : IRequestHandler<ProductClassificationByCountryQuery,IEnumerable<ProductClassificationByCountryDTO>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductClassificationByCountryQueryHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<ProductClassificationByCountryDTO>> Handle(ProductClassificationByCountryQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ProductClassificationByCountry(request.page,request.pageSize,request.Type);
        }
    }
}
