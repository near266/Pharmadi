using AutoMapper;
using Jhipster.Service.Utilities;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;

namespace Module.Ordering.Application.Queries.OrderItemQ
{
    public class OrderItemGetAllByOrderAdminQuery : IRequest<PagedList<OrderItem>>
    {
        public Guid purchaseOrderId { get; set; }
    }
    public class OrderItemGetAllByOrderAdminQueryHandler : IRequestHandler<OrderItemGetAllByOrderAdminQuery, PagedList<OrderItem>>
    {
        private readonly IOrderItemRepostitory _repo;
        private readonly IMapper _mapper;
        public OrderItemGetAllByOrderAdminQueryHandler(IOrderItemRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedList<OrderItem>> Handle(OrderItemGetAllByOrderAdminQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllItemByOrderAdmin(request.purchaseOrderId);
        }
    }

}
