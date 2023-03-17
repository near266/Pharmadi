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
        public Guid WarehouseId { get; set; }
        [Column("Ton kho")]
        public int AvailabelQuantity { get; set; }
        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
