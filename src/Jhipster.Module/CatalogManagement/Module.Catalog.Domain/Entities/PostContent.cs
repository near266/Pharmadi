using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Catalog.Domain.Entities
{
    public class PostContent : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string>? Images { get; set; }
        public List<string>? Videos { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
