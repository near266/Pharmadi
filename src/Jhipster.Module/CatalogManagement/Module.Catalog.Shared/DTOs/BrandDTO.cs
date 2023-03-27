﻿    using Module.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Catalog.Shared.DTOs
{
    public class BrandDTO
    {
        public Guid Id { get; set; }
        public Guid? GroupBrandId { get; set; }
        public string Intro { get; set; }
        public string BrandName { get; set; }
        public string LogoBrand { get; set; }
        public bool? Pin { get; set; }
        public GroupBrand? GroupBrand { get; set; }
        public int SumProduct { get; set; }
    }
    public class AddBrandDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public Guid GroupBrandId { get; set; }
        public string LogoBrand { get; set; }
        public string? Intro { get; set; }
        public bool? Pin { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}
