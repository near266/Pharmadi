using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Shared.DTOs
{
    public class ProductSearchDTO
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public List<string>? Image { get; set; }
        public string? Specification { get; set; }
        public IEnumerable<LabelProduct>? LabelProducts { get; set; }
        public IEnumerable<CategoryProduct>? CategoryProducts { get; set; }
        public Brand? Brands { get; set; }
        public int? SaleNumber { get; set; }
        public string CartNumber { get; set; } = "0";
        public bool? Archived { get; set; }
        [JsonIgnore]
        public float? Discount { get; set; }
        public bool? CanOrder { get; set; }
        public string? ShortName { get; set; }


    }
    public class ViewProductPromotionDTO
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public List<string>? Image { get; set; }
        public string? Specification { get; set; }
        public IEnumerable<LabelProduct>? LabelProducts { get; set; }
        public IEnumerable<CategoryProduct>? CategoryProducts { get; set; }
        public Brand? Brands { get; set; }
        public int? SaleNumber { get; set; }
        public string CartNumber { get; set; } = "0";
        public bool? Archived { get; set; }
        [JsonIgnore]
        public float? Discount { get; set; }
        public bool? CanOrder { get; set; }
        public string? ShortName { get; set; }
        public string? Country { get; set; }

        public int? ImportedProducts { get; set; }


    }
    public class SearchProductDTO
    {
        public Guid Id { get; set; }
        public string SKU { get; set; }

        public string ProductName { get; set; }
        public string? Specification { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Description { get; set; }
        public int? Quantitysold { get; set; }
        public string? UnitName { get; set; }
        public int Status { get; set; }
        public List<string>? Image { get; set; }
    }
    public class SearchProductBrandId
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string? Descripton { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}
