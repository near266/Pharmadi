using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;



namespace Module.Ordering.Application.Queries.OrderItemQ
{
    public class OrderItemGetAllByOrderQuery : IRequest<PagedList<OrderItem>>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public Guid purchaseOrderId { get; set; }
    }
    public class OrderItemGetAllByOrderQueryHandler : IRequestHandler<OrderItemGetAllByOrderQuery, PagedList<OrderItem>>
    {
        private readonly IOrderItemRepostitory _repo;
        private readonly IMapper _mapper;
        public OrderItemGetAllByOrderQueryHandler(IOrderItemRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<OrderItem>> Handle(OrderItemGetAllByOrderQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllItemByOrder(request.page, request.pageSize, request.purchaseOrderId);
        }
    }

}
