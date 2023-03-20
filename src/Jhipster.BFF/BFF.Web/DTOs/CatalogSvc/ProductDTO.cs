using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }

    }
    public class ViewListProductDTo {
    
    public IEnumerable<ProductListDTO>? ProductList { get; set; }
    }

}
