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

namespace Module.Ordering.Application.Commands.PurchaseOrderCm
{
    public class PurchaseOrderUpdateCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? OrderCode { get; set; }
        public Guid? MerchantId { get; set; }
        public string? MerchantName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ContactName { get; set; }
        public string? ContractNumber { get; set; }
        public Decimal? ShippingFee { get; set; }
        public string? LinkShipping { get; set; }
        public Decimal? TotalPrice { get; set; }
        public Decimal? TotalPayment { get; set; }
        public int? Status { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string? Description { get; set; }

    }
    public class PurchaseOrderUpdateCommandHandler : IRequestHandler<PurchaseOrderUpdateCommand, int>
    {
        private readonly IPurchaseOrderRepostitory _repo;
        private readonly IMapper _mapper;
        public PurchaseOrderUpdateCommandHandler(IPurchaseOrderRepostitory repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Handle(PurchaseOrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<PurchaseOrder>(request);
            return await _repo.Update(obj);
        }
    }

}
