using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
namespace Module.Ordering.Application.Commands.PurchaseOrderCm
{
    public class PurchaseOrderAddCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class PurchaseOrderAddCommandHandler : IRequestHandler<PurchaseOrderAddCommand, int>
    {
        private readonly IPurchaseOrderRepostitory _repo;
        private readonly IMapper _mapper;
        public PurchaseOrderAddCommandHandler(IPurchaseOrderRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(PurchaseOrderAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<PurchaseOrder>(request);
            return await _repo.Add(obj);
        }
    }

}
