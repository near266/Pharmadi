using System.Runtime.Serialization;

namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class ProductAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string SKU { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string ProductName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string? Function { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public decimal? ListPrice { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public decimal? SalePrice { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string? Description { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? UnitName { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? BrandId { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public int Status { get; set; }

        [DataMember(Order = 11, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 12, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class ProductUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string SKU { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string ProductName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string? Function { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public decimal? ListPrice { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public decimal? SalePrice { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string? Description { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? UnitName { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public Guid? BrandId { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public int Status { get; set; }

        [DataMember(Order = 11, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 12, IsRequired = false)]
        public DateTime? LastModifiedDate { get; set; }
    }

    [DataContract]
    public class ProductDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }

    [DataContract]
    public class ProductBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class ProductGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string SKU { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string ProductName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string? Function { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public decimal? ListPrice { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public decimal? SalePrice { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string? Description { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? UnitName { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public BrandC? Brand { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public IEnumerable<LabelProductC> LabelProducts { get; set; }
        [DataMember(Order = 11, IsRequired = false)]
        public IEnumerable<TagProductC> TagProducts { get; set; }
    }

    [DataContract]
    public class ProductGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }   
    }

    [DataContract]
    public class ProductSearchListRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class ProductSearchRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public List<Guid?>? categoryIds { get; set; }
        [DataMember(Order = 2, IsRequired =false)]
        public List<Guid?>? brandIds { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public List<Guid?>? tagIds { get; set; }
        [DataMember(Order = 4, IsRequired =false)]
        public string? keyword { get; set; }
        [DataMember(Order = 5, IsRequired =false)]
        public int page { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class ProductInforSearchResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string SKU { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string ProductName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public decimal? ListPrice { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public decimal? SalePrice { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public string? UnitName { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public BrandC? Brand { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public IEnumerable<LabelProductC> LabelProducts { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public IEnumerable<TagProductC> TagProducts { get; set; }
    }

    [DataContract]
    public class ProductViewDetailResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string SKU { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string ProductName { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public string? Function { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public decimal? ListPrice { get; set; }
        [DataMember(Order = 6, IsRequired = false)]
        public decimal? SalePrice { get; set; }
        [DataMember(Order = 7, IsRequired = false)]
        public string? Description { get; set; }
        [DataMember(Order = 8, IsRequired = false)]
        public string? UnitName { get; set; }
        [DataMember(Order = 9, IsRequired = false)]
        public BrandC? Brand { get; set; }
        [DataMember(Order = 10, IsRequired = false)]
        public IEnumerable<LabelProductC> LabelProducts { get; set; }
        [DataMember(Order = 11, IsRequired = false)]
        public IEnumerable<TagProductC> TagProducts { get; set; }
    }

    [DataContract]
    public class ProductViewDetailRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid id { get; set; }
    }

}
