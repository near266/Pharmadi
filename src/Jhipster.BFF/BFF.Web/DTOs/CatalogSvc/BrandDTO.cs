using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    public class AddBrandToGroupRequest
    {
        public List<Guid> BrandIds { get; set; }
        public Guid GroupId { get; set; }
    }
}
