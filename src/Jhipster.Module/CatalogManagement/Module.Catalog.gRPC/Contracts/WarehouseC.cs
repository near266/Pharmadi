﻿using System.Runtime.Serialization;

namespace Module.Catalog.gRPC.Contracts
{
    [DataContract]
    public class WarehouseAddRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string WarehouseName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class WarehouseUpdateRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string WarehouseName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? LastModifiedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime? LastModifiedDate { get; set; }
    }

    [DataContract]
    public class WarehouseDeleteRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
    }


    [DataContract]
    public class WarehouseBaseResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int message { get; set; }
    }

    [DataContract]
    public class WarehouseInfor
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string WarehouseName { get; set; }
        [DataMember(Order = 3, IsRequired = false)]
        public string? Descripton { get; set; }
        [DataMember(Order = 4, IsRequired = false)]
        public Guid? CreatedBy { get; set; }
        [DataMember(Order = 5, IsRequired = false)]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class WarehouseGetAllAdminResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string WarehouseName { get; set; }
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
    public class WarehouseGetAllAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public int page { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public int pageSize { get; set; }
    }

    [DataContract]
    public class WarehouseSearchRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string? keyword { get; set; }
    }

    [DataContract]
    public class WarehouseSearchResponse
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; }
        [DataMember(Order = 2, IsRequired = false)]
        public string WarehouseName { get; set; }

    }

}
