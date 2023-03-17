using System.Runtime.Serialization;

namespace Module.Factor.gRPC.Contracts
{
    [DataContract]
    public class MerchantAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string? TaxCode { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string MerchantName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string PhoneNumber { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public string Address { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public string Location { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string ContactName { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? GPPNumber { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class MerchantUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string? TaxCode { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string MerchantName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string PhoneNumber { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public string Address { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public string Location { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string ContactName { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? GPPNumber { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }

    [DataContract]
    public class MerchantDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }

    [DataContract]
    public class MerchantBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class MerchantInforResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string? TaxCode { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string MerchantName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string PhoneNumber { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public string Address { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public string Location { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string ContactName { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? GPPNumber { get; set; }
    }

    [DataContract]
    public class MerchantGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string? TaxCode { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string MerchantName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string PhoneNumber { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public string Address { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public string Location { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string ContactName { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? GPPNumber { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
        [DataMember(Order = 11, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 12, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }

    }

    [DataContract]
    public class MerchantGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? keyword { get; set; }

    }

    [DataContract]
    public class MerchantViewDetailRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid id { get; set; }

    }
}
