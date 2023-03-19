using AutoMapper;
using MediatR;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Commands.OrderItemCm
{
    public class OrderItemAddCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderItemAddCommandHandler : IRequestHandler<OrderItemAddCommand, int>
    {
        private readonly IOrderItemRepostitory _repo;
        private readonly IMapper _mapper;
        public OrderItemAddCommandHandler(IOrderItemRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(OrderItemAddCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<OrderItem>(request);
            return await _repo.Add(obj);
        }
    }

}
