using System.Runtime.Serialization;

namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class TagAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string TagName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class TagUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string TagName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime? LastModifiedDate { get; set; }
    }

    [DataContract]
    public class TagDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class TagBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class TagInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string TagName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class TagGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string TagName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public DateTime LastModifiedDate { get; set; }
    }


    [DataContract]
    public class TagGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class TagSearchRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string? keyword { get; set; }
    }

    [DataContract]
    public class TagSearchResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string TagName { get; set; }

    }

}
