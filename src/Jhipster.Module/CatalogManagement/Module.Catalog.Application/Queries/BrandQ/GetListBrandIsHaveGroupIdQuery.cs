﻿using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Entities;
using Module.Catalog.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Application.Queries.BrandQ
{
    public class GetListBrandIsHaveGroupIdQuery : IRequest<PagedList<IsHaveGroupDTO>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int type { get; set; }
        public Guid? GroupBrandId { get; set; } 
        public Guid? UserId { get; set; }
    }
    public class GetListBrandIsHaveGroupIdQueryHandler : IRequestHandler<GetListBrandIsHaveGroupIdQuery, PagedList<IsHaveGroupDTO>>
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public GetListBrandIsHaveGroupIdQueryHandler(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<IsHaveGroupDTO>> Handle(GetListBrandIsHaveGroupIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.IsHaveGroup(request.page,request.pageSize,request.type,request.GroupBrandId,request.UserId);
        }
    }
}
