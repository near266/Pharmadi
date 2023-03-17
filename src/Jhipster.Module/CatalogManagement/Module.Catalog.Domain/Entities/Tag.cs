using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Domain.Entities
{
    public class Tag : BaseEntity<Guid>
    {
        [MaxLength(100)]
        //[Column("Ten tag ")]
        public string TagName { get; set; }
    }
}
