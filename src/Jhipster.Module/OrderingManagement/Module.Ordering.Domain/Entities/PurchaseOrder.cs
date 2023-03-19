using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Domain.Entities
{
    public class PurchaseOrder:BaseEntity<Guid>
    {
        public Guid MerchantId { get; set; }
        public Decimal ShippingFee { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalPayment { get; set; }
        public int Status { get; set; }
        public Merchant Merchant { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
