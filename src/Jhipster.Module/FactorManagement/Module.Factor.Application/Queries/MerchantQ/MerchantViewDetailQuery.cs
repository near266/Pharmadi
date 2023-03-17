using AutoMapper;
using MediatR;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Entities;

namespace Module.Factor.Application.Queries.MerchantQ
{
    public class MerchantViewDetailQuery : IRequest<Merchant>
    {
        public Guid Id { get; set; }
    }
    public class MerchantViewDetailQueryHandler : IRequestHandler<MerchantViewDetailQuery, Merchant>
    {
        private readonly IMerchantRepository _repo;
        private readonly IMapper _mapper;
        public MerchantViewDetailQueryHandler(IMerchantRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Merchant> Handle(MerchantViewDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewDetail(request.Id);
        }
    }

}
