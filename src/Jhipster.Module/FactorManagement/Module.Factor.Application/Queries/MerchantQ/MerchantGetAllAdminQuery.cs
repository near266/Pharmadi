﻿

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using System.Xml.Linq;

namespace Module.Factor.Application.Queries.MerchantQ
{
    public class MerchantGetAllAdminQuery : IRequest<PagedList<Merchant>>
    {

        public int page { get; set; }
        public int pageSize { get; set; }
        public string? name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
    }
    public class MerchantGetAllAdminQueryHandler : IRequestHandler<MerchantGetAllAdminQuery, PagedList<Merchant>>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public MerchantGetAllAdminQueryHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<Merchant>> Handle(MerchantGetAllAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAdmin(request.page, request.pageSize, request.name, request.StartDate, request.EndDate, request.Status);
        }
    }

}
