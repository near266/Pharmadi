using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Module.Catalog.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        [MaxLength(10)]
        public string SKU { get; set; }
        [MaxLength(100)]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string? Function { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        [MaxLength(20)]
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int Status { get; set; }
        public Guid? PostContentId { get; set; }
        public bool? HideProduct { get; set; }
        public List<string>? Image { get; set; }
        public string? Industry { get; set; }
        public string? Effect { get;set; }
        public string? Preserve { get; set; }
        public string? Dosage { get; set; }
        public string? DosageForms { get; set; }
        public string? Country { get; set; }
        public string? Ingredient { get; set; }
        public string? Usage { get; set; }
        public string? Specification { get; set; }
        public int? Number { get; set; }
        public bool Archived { get; set; }
        public PostContent? PostContent { get; set; }
        public IEnumerable<LabelProduct> LabelProducts { get; set; }
        public IEnumerable<TagProduct> TagProducts { get; set; }
        public IEnumerable<CategoryProduct> CategoryProducts { get; set; }
        public IEnumerable<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
