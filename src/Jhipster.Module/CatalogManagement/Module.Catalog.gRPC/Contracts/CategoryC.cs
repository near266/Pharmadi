using Module.Catalog.gRPC.Contracts.PagedListC;
using ProtoBuf.Meta;
using System.Runtime.Serialization;


namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class CategoryAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string CategoryName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? ParentId { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public bool IsLeaf { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public DateTime CreatedDate { get; set; }

    }

    [DataContract]
    public class CategoryUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string CategoryName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? ParentId { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public bool IsLeaf { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public DateTime? LastModifiedDate { get; set; }

    }

    [DataContract]
    public class CategoryDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class CategoryBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class CategoryInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string CategoryName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? ParentId { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public bool IsLeaf { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public DateTime CreatedDate { get; set; }

    }

    [DataContract]
    public class CategoryGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string CategoryName { get; set; }
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
    public class CategoryGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class CategorySearchRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string? keyword { get; set; }
    }

    [DataContract]
    public class CategorySearchResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string CategoryName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public IEnumerable<CategoryC> Categorys { get; set; }
       
    }

}
