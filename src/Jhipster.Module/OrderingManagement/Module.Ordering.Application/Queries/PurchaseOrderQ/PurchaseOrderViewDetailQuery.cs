using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using Module.Ordering.Shared.DTOs;

namespace Module.Ordering.Application.Queries.PurchaseOrderQ
{
    public class PurchaseOrderViewDetailQuery : IRequest<PurchaseOrderViewDetailDTO>
    {

        public Guid id { get; set; }
    }
    public class PurchaseOrderViewDetailQueryHandler : IRequestHandler<PurchaseOrderViewDetailQuery, PurchaseOrderViewDetailDTO>
    {
        private readonly IPurchaseOrderRepostitory _repo;
        private readonly IMapper _mapper;
        public PurchaseOrderViewDetailQueryHandler(IPurchaseOrderRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PurchaseOrderViewDetailDTO> Handle(PurchaseOrderViewDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ViewDetail(request.id);
        }
    }

}
