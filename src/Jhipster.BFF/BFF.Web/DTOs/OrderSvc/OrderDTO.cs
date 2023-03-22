

using System.Text.Json.Serialization;

namespace BFF.Web.DTOs.OrderSvc
{
    public class OrderAddRequestAdmin
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string OrderCode { get; set; }
        public Guid MerchantId { get; set; }
        public Decimal ShippingFee { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalPayment { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        public List<OrderItemRequest> orderItemRequests { get; set; }
    }

    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderAddRequestUser
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string OrderCode { get; set; }
        public Guid MerchantId { get; set; }
        public Decimal ShippingFee { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal TotalPayment { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }

}
