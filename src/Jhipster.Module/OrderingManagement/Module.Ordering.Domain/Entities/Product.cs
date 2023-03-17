
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Module.Ordering.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }    
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
        public int Status { get; set; }
    }
}
