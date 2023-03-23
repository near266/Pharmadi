using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }

    }
    public class ViewListProductDTo 
    {
    
    public IEnumerable<ProductListDTO>? ProductList { get; set; }
    }

    public class ProductAddRequest
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string? Function { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Description { get; set; }
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public int Status { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? PostContentId { get; set; }
        public bool? HideProduct { get; set; }
        public List<string>? Image { get; set; }
        public bool? Archived { get; set; }
        public string? Industry { get; set; }
        public string? Effect { get; set; }
        public string? Preserve { get; set; }
        public string? Dosage { get; set; }
        public string? DosageForms { get; set; }
        public string? Country { get; set; }
        public string? Ingredient { get; set; }
        public string? Usage { get; set; }
        public string? Specification { get; set; }
        public int? Number { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime CreatedDate { get; set; }
        public List<CategoryProductAddRequest>? categoryProductAdds { get; set; }
        public List<WarehouseProductAddRequest>? warehouseProductAdds { get; set; }
        public List<Guid>? TagIds { get; set; }
        public List<Guid>? LabelIds { get; set; }
 

    }

    public class WarehouseProductAddRequest
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public Guid ProductId { get; set; }
        public string Lot { get; set; }
        public DateTime DateExpire { get; set; }
        public int AvailabelQuantity { get; set; }
    }

    public class CategoryProductAddRequest
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid Id { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public bool Priority { get; set; }
    }
    public class SearchProductInforDTO {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string? Specification { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Description { get; set; }
        public int?Quantitysold { get; set; }
        public string? UnitName { get; set; }
        public int Status { get; set; }
        public List<string>? Image { get; set; }
    }

    public class GetAllAminDTO
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string? Function { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int Status { get; set; }
        public int Number { get; set; }  
    }
    //public class ProductUpdateRequest
    //{
    //    public Guid Id { get; set; }
    //    public string? SKU { get; set; }
    //    public string? ProductName { get; set; }
    //    public string? Function { get; set; }
    //    public decimal? Price { get; set; }
    //    public decimal? SalePrice { get; set; }
    //    public string? Description { get; set; }
    //    public string? UnitName { get; set; }
    //    public Guid? BrandId { get; set; }
    //    public int? Status { get; set; }
    //    public Guid? PostContentId { get; set; }
    //    public bool? HideProduct { get; set; }
    //    public string? Image { get; set; }
    //    public string? Industry { get; set; }
    //    public string? Effect { get; set; }
    //    public string? Preserve { get; set; }
    //    public string? Dosage { get; set; }
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public Guid? LastModifiedBy { get; set; }
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public DateTime LastModifiedDate { get; set; }
    //    public Guid? CategoryId { get; set; }
    //}



}
