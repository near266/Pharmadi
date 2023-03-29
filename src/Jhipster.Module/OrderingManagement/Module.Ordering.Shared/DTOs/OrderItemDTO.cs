using Module.Catalog.Domain.Entities;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Shared.DTOs
{
    
    public class OrderItemResponse
    {
        public Brand Brand { get; set; }
        public List<OrderItem> OrderItems{get;set;}
        public int BrandQuantity { get; set; }
    }
}
