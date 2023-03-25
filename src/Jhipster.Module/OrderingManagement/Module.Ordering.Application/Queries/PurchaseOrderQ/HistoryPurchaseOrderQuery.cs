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
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid id { get; set; }
        public int? Status { get; set; }
        public string? OrderCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? NameProduct { get; set; }

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
            return await _purchaseOrderRepostitory.transactionHistory(request.id,request.Status,request.OrderCode,request.CreatedDate,request.NameProduct);
        }
    }
}
