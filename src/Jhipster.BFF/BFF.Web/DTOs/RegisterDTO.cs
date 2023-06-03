using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFF.Web.Constants;
using Newtonsoft.Json;

namespace BFF.Web.DTOs
{
    //public class RegisterByUserDTO
    //{
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public Guid? Id { get; set; }
    //    public string Login { get; set; }

    //    public string? Email { get; set; }

    //    private string? _langKey;
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public string? LangKey
    //    {
    //        get { return _langKey; }
    //        set { _langKey = value; if (string.IsNullOrEmpty(_langKey)) _langKey = "en"; }
    //    }
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public string? CreatedBy { get; set; }
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public DateTime? CreatedDate { get; set; }
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public ISet<string>? Roles { get; set; }

    //    public const int PasswordMinLength = 4;

    //    public const int PasswordMaxLength = 100;

    //    public string Password { get; set; }

    //    public string? PhoneNumber { get; set; }

    //    public string? TaxCode { get; set; }
    //    public string MerchantName { get; set; }
    //    public string Address { get; set; }
    //    public string Location { get; set; }
    //    public string ContactName { get; set; }
    //    public string? GPPNumber { get; set; }
    //    public string? ContractNumber { get; set; }
    //    public int? Channel { get; set; }
    //    public int? Rank { get; set; }
    //    public string? Branch { get; set; }
    //    public string? TypeCustomer { get; set; }
    //    [System.Text.Json.Serialization.JsonIgnore]
    //    public int? Status { get; set; }

    //}
    public class RegisterByUserDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? Id { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Login { get; set; }

        public string? Email { get; set; }

        private string? _langKey;
        [System.Text.Json.Serialization.JsonIgnore]
        public string? LangKey
        {
            get { return _langKey; }
            set { _langKey = value; if (string.IsNullOrEmpty(_langKey)) _langKey = "en"; }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        public string? CreatedBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ISet<string>? Roles { get; set; }

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 100;

        public string? Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<string>? Pdf { get; set; }
        public string? PhoneNumber { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? UtmId { get; set; }
        public string? Utmlink { get; set; }
        public string? Campaign { get; set; }
        public string? Content { get; set; }
        public string? Medium { get; set; }
        public string? Source { get; set; }
        public DateTime? DateLogin { get; set; }
        public DateTime? DateRegister { get; set; }



    }

    public class RegisterByAdminDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? Id { get; set; }
        public string Login { get; set; }

        public string? Email { get; set; }

        private string _langKey;
        [System.Text.Json.Serialization.JsonIgnore]
        public string? LangKey
        {
            get { return _langKey; }
            set { _langKey = value; if (string.IsNullOrEmpty(_langKey)) _langKey = "en"; }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        public string? CreatedBy { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        public ISet<string>? Roles { get; set; }

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 100;

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? TaxCode { get; set; }
        public string MerchantName { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
        public string? ContactName { get; set; }
        public string? GPPNumber { get; set; }
        public string? ContractNumber { get; set; }
        public int? Channel { get; set; }
        public int? Rank { get; set; }
        public string? Branch { get; set; }
        public string? TypeCustomer { get; set; }

        public string? City { get; set; }
        public string? District { get; set; }

        public string? SubDistrict { get; set; }
        public DateTime? LicenseDate { get; set; }
        public string? LicensePlace { get; set; }

        public List<string>? GPPImage { get; set; }

        public string? Avatar { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]

        public int? Status { get; set; }
        public int? AddressStatus { get; set; }

    }
}
