using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Domain.Entities
{
    public class GroupBrand : BaseEntity<Guid>
    {
        [MaxLength(100)]
        //[Column("Ten to chuc")]
        public string GroupBrandName { get; set; }
        public string? LogoGroupBrand { get; set; }
        public bool? Pin { get; set; }
    }
}
