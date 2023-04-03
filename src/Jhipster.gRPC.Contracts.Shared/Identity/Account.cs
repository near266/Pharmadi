// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Jhipster.Crosscutting.Constants;
using Newtonsoft.Json;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Jhipster.gRPC.Contracts.Shared.Identity
{
    #region 1.Register User
    [DataContract]
    public class RegisterRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Id { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        [Required]
        [RegularExpression(Constants.LoginRegex)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Login { get; set; }

        [DataMember(Order = 3, IsRequired = false)]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Email { get; set; }

        private string? _langKey;

        [DataMember(Order = 4, IsRequired = false)]
        [MinLength(2)]
        [MaxLength(6)]
        public string LangKey
        {
            get { return _langKey; }
            set { _langKey = value; if (string.IsNullOrEmpty(_langKey)) _langKey = Constants.DefaultLangKey; }
        }

        [DataMember(Order = 5, IsRequired = false)]
        public string? CreatedBy { get; set; }

        [DataMember(Order = 6, IsRequired = false)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Order = 7, IsRequired = false)]
        [JsonProperty(PropertyName = "authorities")]
        [JsonPropertyName("authorities")]
        public ISet<string> Roles { get; set; }

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 100;

        [DataMember(Order = 8, IsRequired = true)]
        [Required]
        [MinLength(PasswordMinLength)]
        [MaxLength(PasswordMaxLength)]
        public string Password { get; set; }

        [DataMember(Order = 9, IsRequired = false)]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }

    [DataContract]
    public class RegisterResponse
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string Id { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        public string Login { get; set; }
        //[DataMember(Order = 3, IsRequired = false)]

        //public string FirstName { get; set; }
        //[DataMember(Order = 4, IsRequired = false)]

        //public string LastName { get; set; }
        //[DataMember(Order = 5, IsRequired = false)]
        //public string Email { get; set; }
        //[DataMember(Order = 6, IsRequired = false)]

        //public string PhoneNumber { get; set; }
        //[DataMember(Order = 7, IsRequired = false)]

        //public string ImageUrl { get; set; }
        //[DataMember(Order = 8, IsRequired = false)]

        //public bool Activated { get; set; }
        //[DataMember(Order = 9, IsRequired = false)]

        //private Guid? _branchId;
        //[DataMember(Order = 10, IsRequired = false)]
        //public Guid? BranchId
        //{
        //    get { return _branchId; }
        //    set { _branchId = value; if (_branchId.ToString().Equals(SystemConstants.GuidDefault)) _branchId = null; }
        //}
        //[DataMember(Order = 11, IsRequired = false)]
        //public string LangKey { get; set; }
        //[DataMember(Order = 12, IsRequired = false)]
        //public string CreatedBy { get; set; }
        //[DataMember(Order = 13, IsRequired = false)]

        //public DateTime? CreatedDate { get; set; }
        //[DataMember(Order = 14, IsRequired = false)]

        //public string LastModifiedBy { get; set; }
        //[DataMember(Order = 15, IsRequired = false)]
        //public DateTime? LastModifiedDate { get; set; }
        ////[DataMember(Order = 16, IsRequired = false)]
        ////[JsonProperty(PropertyName = "authorities")]
        ////[JsonPropertyName("authorities")]
        ////public ISet<string> Roles { get; set; }
    }
    #endregion

    #region 2.Register User (Admin)
    [DataContract]
    public class RegisterAdminRequest
    {
        [DataMember(Order = 1, IsRequired = false)]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Id { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        [Required]
        [RegularExpression(Constants.LoginRegex)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Login { get; set; }

        [DataMember(Order = 3, IsRequired = false)]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(50)]
        public string? Email { get; set; }

        private string? _langKey;

        [DataMember(Order = 4, IsRequired = false)]
        [MinLength(2)]
        [MaxLength(6)]
        public string? LangKey
        {
            get { return _langKey; }
            set { _langKey = value; if (string.IsNullOrEmpty(_langKey)) _langKey = Constants.DefaultLangKey; }
        }

        [DataMember(Order = 5, IsRequired = false)]
        public string? CreatedBy { get; set; }

        [DataMember(Order = 6, IsRequired = false)]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Order = 7, IsRequired = false)]
        [JsonProperty(PropertyName = "authorities")]
        [JsonPropertyName("authorities")]
        public ISet<string> Roles { get; set; }

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 100;

        [DataMember(Order = 8, IsRequired = true)]
        [Required]
        [MinLength(PasswordMinLength)]
        [MaxLength(PasswordMaxLength)]
        public string Password { get; set; }

        [DataMember(Order = 9, IsRequired = false)]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        
    }

    [DataContract]
    public class RegisterAdminResponse
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string? Id { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        [Required]
        [RegularExpression(Constants.LoginRegex)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Login { get; set; }
        //[DataMember(Order = 3, IsRequired = true)]
        //public string FirstName { get; set; }
        //[DataMember(Order = 4, IsRequired = true)]
        //public string LastName { get; set; }
        //[DataMember(Order = 5, IsRequired = true)]
        //[EmailAddress]
        //[MinLength(5)]
        //[MaxLength(50)]
        //public string? Email { get; set; }
        //[DataMember(Order = 6, IsRequired = false)]
        //[MaxLength(20)]
        //public string? PhoneNumber { get; set; }

        //[DataMember(Order = 7, IsRequired = false)]
        //[MinLength(2)]
        //[MaxLength(6)]
        //public string LangKey { get; set; }
        //[DataMember(Order = 8, IsRequired = false)]
        //public string? Description { get; set; }
        //[DataMember(Order = 9, IsRequired = false)]
        //public DateTime? DOB { get; set; }
        //[DataMember(Order = 10, IsRequired = false)]
        //public int? Gender { get; set; }
        //[DataMember(Order = 11, IsRequired = false)]
        //public string? Zipcode { get; set; }

        //[DataMember(Order = 12, IsRequired = false)]
        //public string? CreatedBy { get; set; }

        //[DataMember(Order = 13, IsRequired = false)]
        //public DateTime? CreatedDate { get; set; }

        //[DataMember(Order = 14, IsRequired = false)]
        //[JsonProperty(PropertyName = "authorities")]
        //[JsonPropertyName("authorities")]
        //public ISet<string> Roles { get; set; }
        //[DataMember(Order = 15, IsRequired = false)]
        //public int Status { get; set; }
        //[DataMember(Order = 16, IsRequired = false)]
        //public ISet<string>? Tags { get; set; }
        //[DataMember(Order = 17, IsRequired = false)]
        //public string? Address { get; set; }
        //[DataMember(Order = 18, IsRequired = false)]
        //public string? NoteAddress { get; set; }
    }
    public class RegisterEmployeeByUserDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Id { get; set; }

        public string? Login { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

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

        public ISet<string>? Roles { get; set; }

        public const int PasswordMinLength = 4;

        public const int PasswordMaxLength = 100;

        public string Password { get; set; }

        public string? PhoneNumber { get; set; }



    }

    #endregion
    public interface IAccountService
    {
        [Operation]
        Task<RegisterResponse> RegisterAccount(RegisterRequest request, CallContext context = default);
        Task<RegisterAdminResponse> RegisterAccountAdmin(RegisterAdminRequest request, CallContext context = default);
        Task<RegisterAdminResponse> RegisterEmployee(RegisterEmployeeByUserDTO request, CallContext context = default);
    }
}
