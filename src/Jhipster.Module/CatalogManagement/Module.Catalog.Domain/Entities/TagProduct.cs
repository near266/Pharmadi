using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Catalog.Domain.Entities
{
    public class TagProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }
        public Product? Product { get; set; }
        public Tag? Tag { get; set; }
    }
}
