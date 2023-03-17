using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Domain.Entities
{
    public class Warehouse : BaseEntity<Guid>
    {
        [MaxLength(100)]
        //[Column("Ten kho")]
        public string WarehouseName { get; set; }
        [MaxLength(1000)]
        //[Column("Mo ta")]
        public string? Description { get; set; }
    }
}
