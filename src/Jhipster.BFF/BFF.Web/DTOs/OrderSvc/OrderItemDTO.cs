using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.OrderSvc
{
    public class OrderItemAddRq
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderItemUpdateRq
    {
        public Guid Id { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
