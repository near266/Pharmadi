using MediatR;
using Module.Ordering.Application.DTO;
using Module.Ordering.Application.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Application.Commands.PurchaseOrderCm
{
    public class PurchaseOrderCheckStatusCommand:IRequest<StatusMerchantDTO>
    {
        public Guid Id {  get; set; }
    }
    public class PurchaseOrderCheckStatusCommandHandler : IRequestHandler<PurchaseOrderCheckStatusCommand, StatusMerchantDTO>
    {
        private readonly IPurchaseOrderRepostitory _purchaseOrderRepostitory;
        public PurchaseOrderCheckStatusCommandHandler(IPurchaseOrderRepostitory purchaseOrderRepostitory)
        {
            _purchaseOrderRepostitory = purchaseOrderRepostitory;
        }

        public async Task<StatusMerchantDTO> Handle(PurchaseOrderCheckStatusCommand request, CancellationToken cancellationToken)
        {
            return await _purchaseOrderRepostitory.CheckStatus(request.Id);
        }
    }
}
