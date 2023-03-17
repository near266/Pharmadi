using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Ordering.Domain.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; }
        [MaxLength(10)]
        //[Column("Ma so thue")]
        public string? TaxCode { get; set; }
        [MaxLength(50)]
        //[Column("Ten hieu thuoc")]
        public string MerchantName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [MaxLength(2000)]
        public string Address { get; set; }
        [MaxLength(2000)]
        public string Location { get; set; }
        [MaxLength(100)]
        //[Column("Ten nguoi lien lac")]
        public string ContactName { get; set; }
        [MaxLength(50)]
        //[Column("So GPP")]
        public string? GPPNumber { get; set; }
    }
}
