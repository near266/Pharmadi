using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Module.Catalog.Domain.Entities
{
    public class Brand : BaseEntity<Guid>
    {
        [MaxLength(100)]
        //[Column("Ten thuong hieu")]
        public string BrandName { get; set; }
        public Guid GroupBrandId { get; set; }
        public string LogoBrand { get; set; }
        public GroupBrand? GroupBrand { get; set; }
    }
}
