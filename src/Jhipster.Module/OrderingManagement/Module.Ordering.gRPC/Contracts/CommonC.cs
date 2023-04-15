using System.Runtime.Serialization;

namespace Module.Ordering.gRPC.Contracts
{

    [DataContract]
    public class ProductC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string ProductName { get; set; }
        [DataMember(Order =3, IsRequired = false)]
        public decimal? ListPrice { get; set; }
        [DataMember(Order =4, IsRequired = false)]
        public decimal? SalePrice { get; set; }
        [DataMember(Order =5, IsRequired = false)]
        public string? UnitName { get; set; }
    }

    [DataContract]
    public class MerchantC
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

}
