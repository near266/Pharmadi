using MediatR;
using Module.Ordering.Application.DTO;
using Module.Ordering.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Queries.PurchaseOrderQ
{
    public class HistoryPurchaseOrderQuery : IRequest<List<HistoryOrderDTO>>
    {
        public Guid id { get; set; }
    }
    public class HistoryPurchaseOrderQueryHandler : IRequestHandler<HistoryPurchaseOrderQuery, List<HistoryOrderDTO>>
    {
        private readonly IPurchaseOrderRepostitory _purchaseOrderRepostitory;
        public HistoryPurchaseOrderQueryHandler(IPurchaseOrderRepostitory purchaseOrderRepostitory)
        {
            _purchaseOrderRepostitory = purchaseOrderRepostitory;
        }

        public async Task<List<HistoryOrderDTO>> Handle(HistoryPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            return await _purchaseOrderRepostitory.transactionHistory(request.id);
        }
    }
}
