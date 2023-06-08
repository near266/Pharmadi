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
        public decimal? TotalPrice { get; set; }
        public decimal? economicalPrice { get;set; }
        public decimal? TotalPayment { get; set; }
    }

    public class ViewCartByBrandDTO
    {
        public Brand Brand { get; set; }
        public List<Cart> Carts { get; set; }
    }
    public class ViewCartDTO
    {
        public List<ViewCartByBrandDTO> data { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal EconomicalPrice { get; set; }
    }
    public class ViewQuickOrder
    {
        public Guid ProductId { get; set;}
        public string ProductName { get; set;}
        public List<string> Image { get; set;}
        public decimal? Price { get; set; }  
        public decimal? DiscountPrice { get; set; }
        public int? Quantity { get; set; } = 0;
        public string BrandName { get;set;}
        public string? ShortName { get; set; }
    }
}
