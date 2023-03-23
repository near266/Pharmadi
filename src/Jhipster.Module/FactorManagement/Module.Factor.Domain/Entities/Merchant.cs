using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Factor.Domain.Entities
{
    public class Merchant : BaseEntity<Guid>
    {
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
        public string? ContractNumber { get; set; }
        public int? Channel { get; set; }       
        public int? Rank { get; set; }
        public string? Branch { get; set; }
        public string? TypeCustomer { get; set; }
        public int? Status { get; set; } // trạng thái active 0-1
        public string? Email { get; set; }

    }
}