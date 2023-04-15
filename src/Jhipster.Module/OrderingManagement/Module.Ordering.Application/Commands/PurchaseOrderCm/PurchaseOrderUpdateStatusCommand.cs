using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;

namespace Module.Ordering.Application.Commands.PurchaseOrderCm
{
    public class PurchaseOrderUpdateStatusCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
    public class PurchaseOrderUpdateStatusCommandHandler : IRequestHandler<PurchaseOrderUpdateStatusCommand, int>
    {
        private readonly IPurchaseOrderRepostitory _repo;
        private readonly IMapper _mapper;
        public PurchaseOrderUpdateStatusCommandHandler(IPurchaseOrderRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(PurchaseOrderUpdateStatusCommand request, CancellationToken cancellationToken)
        {
            return await _repo.UpdateStatus(request.Id, request.Status);
        }
    }

}
