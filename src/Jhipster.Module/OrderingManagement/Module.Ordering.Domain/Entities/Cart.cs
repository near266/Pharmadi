using Module.Catalog.Domain.Entities;

namespace Module.Ordering.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }  
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        public Product Product { get; set; }
        public bool? IsChoice { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
