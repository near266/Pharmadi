using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Module.Catalog.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string SKU { get; set; }
        [MaxLength(1000)]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string? UserObject { get; set; }
        public decimal? SuggestPrice { get; set; }
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
        public string? Warning { get; set; }
        public string? Preserve { get; set; }
        public string? Dosage { get; set; }
        public string? DosageForms { get; set; }
        public string? Country { get; set; }
        public string? Ingredient { get; set; }
        public string? Usage { get; set; }
        public string? Specification { get; set; }
        public int? Number { get; set; }
        public bool? Archived { get; set; }
        public bool? CanOrder { get; set; }
        public int? NewProduct { get; set; }
        public int? ImportedProducts { get; set; }
        public int? sellingProducts{get;set;}
        public string? ShortName { get; set; }
        public PostContent? PostContent { get; set; }
        public IEnumerable<LabelProduct> LabelProducts { get; set; }
        public IEnumerable<TagProduct> TagProducts { get; set; }
        public IEnumerable<CategoryProduct> CategoryProducts { get; set; }
        public IEnumerable<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
