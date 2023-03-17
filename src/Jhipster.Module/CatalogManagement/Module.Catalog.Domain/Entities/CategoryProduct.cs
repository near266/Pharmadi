using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Catalog.Domain.Entities
{
    public class CategoryProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public bool Priority { get; set; }
        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
