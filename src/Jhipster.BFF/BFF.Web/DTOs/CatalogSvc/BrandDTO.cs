using BFF.Web.DTOs.CatalogSvc;
using Module.Catalog.Domain.Entities;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    public class GroupBrandDTOs
    {


        [JsonIgnore]
        public Guid Id { get; set; }
        public string GroupBrandName { get; set; }
        public string? LogoGroupBrand { get; set; }
        public bool? Pin { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    
    
    }
    public class BrandDTOs
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        [JsonIgnore]
        public Guid? GroupBrandId { get; set; }
        public string LogoBrand { get; set; }
        public string Intro { get; set; }
        public bool? Pin { get; set; }
        public bool? Archived { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }

    //public class AddBrandDTOs
    //{
    //    [JsonIgnore]
    //    public Guid Id { get; set; }
    //    public string BrandName { get; set; }
    //    public Guid GroupBrandId { get; set; }
    //    public string LogoBrand { get; set; }
    //    public string? Intro { get; set; }
    //    public bool? Pin { get; set; }
    //    [JsonIgnore]
    //    public Guid? CreatedBy { get; set; }
    //    [JsonIgnore]
    //    public DateTime CreatedDate { get; set; }
    //}

    public class AddUpdateDeleteRequest
    {
        public Guid GroupId { get; set; }
        public string GroupBrandName { get; set; }
        public string? LogoGroupBrand { get; set; }
        public bool? Pin { get; set; }
     
        public List<AddDeleteUpdateBrand?>? Brands { get; set; }
    
    }

   
    public class AddDeleteUpdateBrand {
        public Guid? Id { get; set; }
        public string BrandName { get; set; }
        [JsonIgnore]
        public Guid? GroupBrandId { get; set; }
        public string? LogoBrand { get; set; }
        public string? Intro { get; set; }
        public bool? Pin { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public Guid? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }

    }
    public class AddBrandToGroupRequest
    {
        public List<AddBrand> BrandIds { get; set; }
        public Guid GroupId { get; set; }
    }
    public class AddBrand
    {
        public Guid Id { get; set; }
    }

    public class CreatNewGroupAndBrandRequest
    {
        public GroupBrandDTOs GroupBrand { get; set; }
        public List<BrandDTOs> Brands { get; set; }
    }

}

