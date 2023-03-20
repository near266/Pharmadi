using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Module.Catalog.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        [MaxLength(10)]
        //[Column("Ma san pham")]
        public string SKU { get; set; }
        [MaxLength(100)]
        //[Column("Ten sp")]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        //[Column("Chuc nang")]
        public string? Function { get; set; }
        //[Column("Gia niem yiet")]
        public decimal? ListPrice { get; set; }
        //[Column("Gia ban")]
        public decimal? SalePrice { get; set; }
        [MaxLength(2000)]
        //[Column("Mo ta")]
        public string? Description { get; set; }
        [MaxLength(20)]
        //[Column("Don vi")]
        public string? UnitName { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int Status { get; set; }
        public Guid? PostContentId { get; set; }
        public bool? HideProduct { get; set; }
        public string? Image { get; set; }
        //[Column("Nganh Hang")]
        public string? Industry { get; set; }
        public string? Effect { get;set; }
        public string? Preserve { get; set; }
        public string? Dosage { get; set; }
        public PostContent? PostContent { get; set; }
        public IEnumerable<LabelProduct> LabelProducts { get; set; }
        public IEnumerable<TagProduct> TagProducts { get; set; }
        public IEnumerable<CategoryProduct> CategoryProducts { get; set; }
    }
}
