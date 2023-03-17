// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Core.Entities
{
    [Table("Functions")]
    public class Function : BaseEntity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public Guid? FunctionTypeId { get; set; }
        public bool? Status { get; set; }

        public FunctionType FunctionType { get; set; }
    }
}
