using System.Runtime.Serialization;

namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class LabelAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string LabelName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class LabelUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string LabelName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime? LastModifiedDate { get; set; }
    }

    [DataContract]
    public class LabelDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class LabelBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class LabelInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string LabelName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class LabelGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string LabelName { get; set; }
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
    public class LabelGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class LabelSearchRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string? keyword { get; set; }
    }

    [DataContract]
    public class LabelSearchResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string LabelName { get; set; }

    }

}
