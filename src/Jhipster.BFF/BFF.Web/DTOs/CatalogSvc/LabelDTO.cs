using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    public class LabelProductAddRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid LabelId { get; set; }
    }

    public class LabelProductUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid LabelId { get; set; }
    }

    public class LabelProductDeleteRequest
    {
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
    }
}
