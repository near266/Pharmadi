

using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;

namespace Module.Factor.Application.Queries.MerchantQ
{
    public class MerchantSearchToChooseQuery : IRequest<IEnumerable<Merchant>>
    {
        public string? keyword { get; set; }
    }
    public class MerchantSearchToChooseQueryHandler : IRequestHandler<MerchantSearchToChooseQuery, IEnumerable<Merchant>>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public MerchantSearchToChooseQueryHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<Merchant>> Handle(MerchantSearchToChooseQuery request, CancellationToken cancellationToken)
        {
            return await _repo.SearchToChoose(request.keyword);
        }
    }

}
