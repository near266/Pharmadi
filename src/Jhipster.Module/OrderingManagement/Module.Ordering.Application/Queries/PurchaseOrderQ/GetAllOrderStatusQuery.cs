using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;



namespace Module.Ordering.Application.Queries.OrderItemQ
{
    public class GetAllOrderStatusQuery : IRequest<IEnumerable<OrderStatus>>
    {
        public Guid purchaseOrderId { get; set; }
    }
    public class GetAllOrderStatusQueryHandler : IRequestHandler<GetAllOrderStatusQuery, IEnumerable<OrderStatus>>
    {
        private readonly IOrderStatusRepostitory _repo;
        private readonly IMapper _mapper;
        public GetAllOrderStatusQueryHandler(IOrderStatusRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<OrderStatus>> Handle(GetAllOrderStatusQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAll(request.purchaseOrderId);
        }
    }

}
