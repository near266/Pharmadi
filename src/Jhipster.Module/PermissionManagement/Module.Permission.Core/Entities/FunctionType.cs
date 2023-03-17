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
    [Table("FunctionTypes")]
    public class FunctionType : BaseEntity<Guid>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public virtual ICollection<Function> Function { get; set; }
    }
}
