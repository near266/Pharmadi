// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Core.Entities
{
    [Table("RoleFunctions")]
    public class RoleFunction : BaseEntity<Guid>
    {
        public string? RoleId { get; set; }
        public Guid? FunctionId { get; set; }
        public bool? Status { get; set; }
        public virtual Function Function { get; set; }
        //public virtual Role Role { get; set; }
    }
}
