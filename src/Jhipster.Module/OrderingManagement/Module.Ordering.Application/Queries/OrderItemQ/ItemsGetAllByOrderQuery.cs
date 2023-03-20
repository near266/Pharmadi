using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;



namespace Module.Factor.Application.Queries.OrderItemQ
{
    public class OrderItemGetAllByUserQuery : IRequest<PagedList<OrderItem>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid purchaseOrderId { get; set; }
    }
    public class OrderItemGetAllByUserQueryHandler : IRequestHandler<OrderItemGetAllByUserQuery, PagedList<OrderItem>>
    {
        private readonly IOrderItemRepostitory _repo;
        private readonly IMapper _mapper;
        public OrderItemGetAllByUserQueryHandler(IOrderItemRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<OrderItem>> Handle(OrderItemGetAllByUserQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllItemByOrder(request.page, request.pageSize, request.purchaseOrderId);
        }
    }

}
