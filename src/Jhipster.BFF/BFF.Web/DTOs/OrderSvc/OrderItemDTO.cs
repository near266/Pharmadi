using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.OrderSvc
{
    public class OrderItemAdd
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderItemUpdate
    {
        public Guid Id { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }

   
    public class OrderItemUpdateRequest

    {
        public List<OrderItemAdd>? orderItemAdds { get; set; }
        public List<OrderItemUpdate>? orderItemUpdates { get; set; }
        public List<Guid>? orderItemDelete { get; set; }
    }
}
