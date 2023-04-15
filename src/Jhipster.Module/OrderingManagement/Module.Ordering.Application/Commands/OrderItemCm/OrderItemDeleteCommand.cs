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
    public class OrderItemDeleteCommand : IRequest<int>
    {
        public List<Guid> Ids { get; set; }    
    }
    public class OrderItemDeleteCommandHandler : IRequestHandler<OrderItemDeleteCommand, int>
    {
        private readonly IOrderItemRepostitory _repo;
        private readonly IMapper _mapper;
        public OrderItemDeleteCommandHandler(IOrderItemRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(OrderItemDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.Ids);
        }
    }

}
