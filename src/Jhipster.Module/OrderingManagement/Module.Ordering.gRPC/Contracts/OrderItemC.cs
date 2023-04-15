using System.Runtime.Serialization;

namespace Module.Ordering.gRPC.Contracts
{
    [DataContract]
    public class OrderItemAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid PurchaseOrderId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid ProductId { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class OrderItemUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid PurchaseOrderId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid ProductId { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class OrderItemDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class OrderItemBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class OrderItemInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public ProductC Product { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class ItemGetAllByOrderRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid purchaseOrderId { get; set; }
    }


}
