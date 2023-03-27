using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Domain.Entities
{
    public class HistoryOrder:BaseEntity<Guid>
    {
        public string OrderCode { get; set; }
        public Guid MerchantId { get; set; }
        public string? MerchantName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ContactName { get; set; }
        public string? ContractNumber { get; set; }
        public Decimal ShippingFee { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalPayment { get; set; }
        public int Status { get; set; }
    }
}
