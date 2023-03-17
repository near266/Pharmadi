// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Application.Dtos
{
    public class RoleFunctionDTO
    {
        public string RoleId { get; set; }
        public Guid FunctionId { get; set; }
        public bool? Status { get; set; }
    }
}
