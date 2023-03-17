// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Application.Dtos
{
    public class FunctionDTO
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public Guid? FunctionTypeId { get; set; }
        public bool? Status { get; set; }
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    public class FunctionAllDTO
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get; set; }
        public ICollection<Function> Function { get; set; }
    }

    public class FunctionListRqDTO
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }
}
