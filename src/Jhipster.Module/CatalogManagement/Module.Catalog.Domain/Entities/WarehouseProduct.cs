using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Catalog.Domain.Entities
{
    public class WarehouseProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Lot { get; set; }
        //public string DateExp { get; set; }
        public DateTime DateExpire { get; set; }
        public int AvailabelQuantity { get; set; }
        public Product? Product { get; set; }
    }
}
