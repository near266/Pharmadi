using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
