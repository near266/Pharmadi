using Jhipster.Service.Utilities;
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
    public class HistoryPurchaseOrderQuery : IRequest<PagedList<HistoryOrderDTO>>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid id { get; set; }
        public int? type { get; set; }
        public int? status { get; set; }
        public string? orderCode { get; set; }
        public string? productKey { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; } 
        public int page { get; set; }
        public int pageSize { get; set; }

    }
    public class HistoryPurchaseOrderQueryHandler : IRequestHandler<HistoryPurchaseOrderQuery, PagedList<HistoryOrderDTO>>
    {
        private readonly IPurchaseOrderRepostitory _purchaseOrderRepostitory;
        public HistoryPurchaseOrderQueryHandler(IPurchaseOrderRepostitory purchaseOrderRepostitory)
        {
            _purchaseOrderRepostitory = purchaseOrderRepostitory;
        }

        public async Task<PagedList<HistoryOrderDTO>> Handle(HistoryPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            return await _purchaseOrderRepostitory.transactionHistory(request.id,request.type,request.status,request.orderCode,request.productKey,request.fromDate, request.toDate, request.page,request.pageSize);
        }
    }
}
