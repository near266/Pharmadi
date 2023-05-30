using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        [MaxLength(50)]
        //[Column("Ten loai")]
        public string CategoryName { get; set; }
        public string? Descripton { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsLeaf { get; set; }
        public string? Image { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<PostContent>? PostContents { get; set; }
    }
}
