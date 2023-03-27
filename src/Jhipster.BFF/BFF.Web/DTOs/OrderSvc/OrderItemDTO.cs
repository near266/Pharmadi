using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
    public class PurchaseOrderUpdateRequest
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
        public Decimal? TotalPrice { get; set; }
        public Decimal? TotalPayment { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public Guid? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime LastModifiedDate { get; set; }
        public OrderItemUpdateRequest OrderItem { get; set; }
    }
}
