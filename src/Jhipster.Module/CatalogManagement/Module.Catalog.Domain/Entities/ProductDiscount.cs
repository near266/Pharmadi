using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Domain.Entities
{
    public class ProductDiscount:BaseEntity<Guid>
    {
       // [Table("Product Sale")]
        public Guid ProductId{ get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public float Discount { get; set; }
        public string Unit { get;set; }
        public Product Product { get; set; }
      
    }
}
