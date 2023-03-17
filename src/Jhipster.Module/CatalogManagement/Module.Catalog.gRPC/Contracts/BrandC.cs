using System.Runtime.Serialization;

namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class BrandAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string BrandName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class BrandUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string BrandName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime? LastModifiedDate { get; set; }
    }

    [DataContract]
    public class BrandDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class BrandBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class BrandInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string BrandName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class BrandGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string BrandName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }


    [DataContract]
    public class BrandGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class BrandSearchRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string? keyword { get; set; }
    }

    [DataContract]
    public class BrandSearchResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string BrandName { get; set; }

    }

}
