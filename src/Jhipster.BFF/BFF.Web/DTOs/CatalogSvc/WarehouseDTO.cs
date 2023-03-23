using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs.CatalogSvc
{
    //public class WarehouseProductAddRequest
    //{
    //    [JsonIgnore]
    //    public Guid Id { get; set; }
    //    public Guid ProductId { get; set; }
    //    public string Lot { get; set; }
    //    public DateTime DateExpire { get; set; }
    //    public int AvailabelQuantity { get; set; }
    //}

    public class WarehouseProductUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Lot { get; set; }
        public DateTime DateExpire { get; set; }
        public int AvailabelQuantity { get; set; }
    }
}
