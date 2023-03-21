﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFF.Web.Constants;

namespace BFF.Web.DTOs
{
    public class RegisterByUserDTO
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? Id { get; set; }
        public string Login { get; set; }

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

        public string? TaxCode { get; set; }
        public string MerchantName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
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
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactName { get; set; }
        public string? GPPNumber { get; set; }
    }
}