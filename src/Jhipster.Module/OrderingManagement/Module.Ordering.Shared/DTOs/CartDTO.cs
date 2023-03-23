using Module.Catalog.Domain.Entities;
using Module.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Shared.DTOs
{
    public class CartResultDTO
    {
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }

    public class ViewCartByBrandDTO
    {
        public Brand Brand { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
