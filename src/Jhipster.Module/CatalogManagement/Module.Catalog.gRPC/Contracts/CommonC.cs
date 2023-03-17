using System.Runtime.Serialization;

namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class BrandC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string BrandName { get; set; }
    }

    [DataContract]
    public class GroupBrandC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string GroupBrandName { get; set; }
    }

    [DataContract]
    public class CategoryC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string CategoryName { get; set; }
    }

    [DataContract]
    public class TagC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string TagName { get; set; }
    }

    [DataContract]
    public class LabelC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string LabelName { get; set; }
    }

    [DataContract]
    public class WarehouseC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string WarehouseName { get; set; }
    }

    [DataContract]
    public class ProductC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string ProductName { get; set; }
    }
    [DataContract]
    public class TagProductC
    {
        [DataMember(Order =1, IsRequired = false)]
        public TagC Tag { get; set; }
    }

    [DataContract]
    public class LabelProductC
    {
        [DataMember(Order = 1, IsRequired = false)]
        public LabelC Label { get; set; }
    }
}
