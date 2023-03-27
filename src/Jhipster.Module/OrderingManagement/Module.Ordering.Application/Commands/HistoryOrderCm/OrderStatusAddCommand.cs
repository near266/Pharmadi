using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Commands.HistoryOrderCm
{
    public class OrderStatusAddCommand:IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public int Status { get; set; }
        public DateTime dateTime { get; set; }

    }
    public class OrderStatusAddCommandHandler : IRequestHandler<OrderStatusAddCommand, int>
    {
        private readonly IOrderStatusRepostitory _repostitory;
        private readonly IMapper _mapper;
        public OrderStatusAddCommandHandler(IOrderStatusRepostitory repostitory,IMapper mapper)
        {
            _mapper = mapper;
            _repostitory = repostitory;
        }

        public async Task<int> Handle(OrderStatusAddCommand request, CancellationToken cancellationToken)
        {
            var tem = _mapper.Map<OrderStatus>(request);
            return await _repostitory.Add(tem);
        }
    }
}
