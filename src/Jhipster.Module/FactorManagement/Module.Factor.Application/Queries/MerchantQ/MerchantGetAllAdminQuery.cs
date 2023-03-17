

using AutoMapper;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;
using Module.Factor.Shared.Utilities;

namespace Module.Factor.Application.Queries.MerchantQ
{
    public class MerchantGetAllAdminQuery : IRequest<PagedList<Merchant>>
    {
        public string? keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
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
            return await _repo.GetAllAdmin(request.page, request.pageSize, request.keyword);
        }
    }

}
