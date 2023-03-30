using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Factor.Domain.Entities
{
    public class Merchant 
    {
        [Key]
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
        public string? Address { get; set; }
        [MaxLength(2000)]
        public string? Location { get; set; }
        [MaxLength(100)]
        //[Column("Ten nguoi lien lac")]
        public string? ContactName { get; set; }
        [MaxLength(50)]
        //[Column("So GPP")]
        public string? GPPNumber { get; set; }
        public string? ContractNumber { get; set; }
        public int? Channel { get; set; }       
        public int? Rank { get; set; }
        public string? Branch { get; set; }
        public string? TypeCustomer { get; set; }
        public int? Status { get; set; } // trạng thái active 1-2
        public string? Email { get; set; }

        public string? City { get; set; }
        public string? District { get; set; }
        
        public string? SubDistrict { get; set; }
        public DateTime? LicenseDate { get; set; }
        public string? LicensePlace { get; set; }

        public List<string>? GPPImage { get; set; }

        public string? Avatar { get; set; }

        public int? AddressStatus { get; set; }// trạng thái phê duyệt địa chỉ 1-2
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}