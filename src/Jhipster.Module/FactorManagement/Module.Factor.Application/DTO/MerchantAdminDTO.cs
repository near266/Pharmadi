using Module.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Factor.Application.DTO
{
    public class UtmDTO {
        public Guid Id { get; set; }
        public string? Utmlink { get; set; }
        public string? Campaign { get; set; }
        public string? Content { get; set; }
        public string? Medium { get; set; }
        public string? Source { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateRegister { get; set; }
    
        public DateTime CreatedDate { get; set; }
       
    }

    public class MerchantAdminDTO
    {
        public Guid Id { get; set; }

        public string? TaxCode { get; set; }
    
        public string MerchantName { get; set; }
     
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
    
        public string? ContactName { get; set; }
     
        public string? GPPNumber { get; set; }
        public string? ContractNumber { get; set; }
        public int? Channel { get; set; }
        public int? Rank { get; set; }
        public string? Branch { get; set; }
        public string? TypeCustomer { get; set; }
        public int? Status { get; set; } 
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
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? Login { get; set; }
        public IEnumerable <Utm>? Utm { get; set; }
    }
}
