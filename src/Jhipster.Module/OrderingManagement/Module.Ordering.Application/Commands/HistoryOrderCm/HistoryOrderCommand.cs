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
    public class HistoryOrderCommand:IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class HistoryOrderCommandHandler : IRequestHandler<HistoryOrderCommand, int>
    {
        private readonly IPurchaseOrderRepostitory _purchaseOrderRepostitory;
        private readonly IMapper _mapper;
        public HistoryOrderCommandHandler(IPurchaseOrderRepostitory purchaseOrderRepostitory,IMapper mapper)
        {
            _mapper = mapper;
            _purchaseOrderRepostitory = purchaseOrderRepostitory;
        }

        public async Task<int> Handle(HistoryOrderCommand request, CancellationToken cancellationToken)
        {
            
            return await _purchaseOrderRepostitory.AddHistoryOrderByPurcharseId(request.Id);
        }
    }
}
