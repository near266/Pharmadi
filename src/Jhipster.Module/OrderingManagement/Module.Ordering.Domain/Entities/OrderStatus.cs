using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Domain.Entities
{
    public class OrderStatus
    {
        public Guid Id { get; set; }
        public Guid  PurchaseOrderId { get; set; }
        public int Status { get; set; }
        public DateTime dateTime { get; set; }
        //public  PurchaseOrder PurchaseOrder { get; set; }
    }
}
