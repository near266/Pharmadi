using System.Runtime.Serialization;

namespace Module.Ordering.gRPC.Contracts
{
    [DataContract]
    public class PurchaseOrderAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid MerchantId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Decimal ShippingFee { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Decimal TotalPrice { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public Decimal TotalPayment { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public int Status { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public DateTime CreatedDate { get; set; }   
    }

    [DataContract]
    public class PurchaseOrderUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid MerchantId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Decimal ShippingFee { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Decimal TotalPrice { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public Decimal TotalPayment { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public int Status { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }

    [DataContract]
    public class PurchaseOrderDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class PurchaseOrderBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class PurchaseOrderInforAdmin
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid MerchantId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Decimal ShippingFee { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Decimal TotalPrice { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public Decimal TotalPayment { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public int Status { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }

    [DataContract]
    public class PurchaseOrderInforUser
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid MerchantId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Decimal ShippingFee { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Decimal TotalPrice { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public Decimal TotalPayment { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public int Status { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }

    [DataContract]
    public class PurchaseOrderInforDetail
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public Guid MerchantId { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Decimal ShippingFee { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Decimal TotalPrice { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public Decimal TotalPayment { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public int Status { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }

    [DataContract]
    public class PurchaseOrderGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public int? status { get; set; }
    }

    [DataContract]
    public class PurchaseOrderGetAllUserRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public int? status { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? userId { get; set; }
    }

    [DataContract]
    public class PurchaseOrderViewDetailRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid id { get; set; }
    }


}
