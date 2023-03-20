using System.Runtime.Serialization;

namespace Module.Factor.gRPC.Contracts
{
    
    public class MerchantAddRequest
    {
        public Guid Id { get; set; }
        public string? TaxCode { get; set; }
        public string MerchantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MerchantUpdateRequest
    {
        public Guid Id { get; set; }
        public string? TaxCode { get; set; }
        public string MerchantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class MerchantDeleteRequest
    {
        public Guid Id { get; set; }
    }

    public class MerchantBaseResponse
    {
        public int message { get; set; }
    }

    public class MerchantInforResponse
    {
       
        public Guid Id { get; set; }
        public string? TaxCode { get; set; }
        public string MerchantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
    }

    public class MerchantGetAllAdminResponse
    {
       
        public Guid Id { get; set; }
        public string? TaxCode { get; set; }
        public string MerchantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }

    public class MerchantGetAllAdminRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string? keyword { get; set; }

    }

    public class MerchantViewDetailRequest
    {
        public Guid id { get; set; }

    }
}
