using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        //public string? Description { get; set; }
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public List<string>? Image { get; set; }
        public string? Specification { get; set; }
        public IEnumerable<LabelProduct>? LabelProducts { get; set; }
        public int SaleNumber { get; set; }
        public string CartNumber { get; set; } ="0";
        public bool? Archived { get; set; }
    }
    public class SearchProductDTO {
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

}
