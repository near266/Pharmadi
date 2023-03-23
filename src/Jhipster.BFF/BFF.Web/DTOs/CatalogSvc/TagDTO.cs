using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    public class TagProductAddRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }
    }

    public class TagProductUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid TagId { get; set; }
    }
}
