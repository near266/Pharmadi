﻿using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Catalog.Domain.Entities;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Queries.CartQ
{
    public class CartGetAllByUserQuery : IRequest<PagedList<ViewCartByBrandDTO>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid userId { get; set; }
    }
    public class CartGetAllByUserQueryHandler : IRequestHandler<CartGetAllByUserQuery, PagedList<ViewCartByBrandDTO>>
    {
        private readonly ICartRepostitory _repo;
        private readonly IMapper _mapper;
        public CartGetAllByUserQueryHandler(ICartRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<ViewCartByBrandDTO>> Handle(CartGetAllByUserQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllByUser(request.page, request.pageSize, request.userId);
        }
    }

}
