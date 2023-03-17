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
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<RoleFunctiondto> RoleFunctions { get; set; }
    }
    public class RoleFunctiondto
    {
        public string RoleId { get; set; }
        //public Guid FunctionId { get; set; }
        public bool Status { get; set; }
        public Functiondto Function { get; set; }

    }
    public class Functiondto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public FunctionTypedto FunctionType { get; set; }
    }
    public class FunctionTypedto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RoleRqDTO
    {
        public string Name { get; set; }
        public List<FunctionListRqDTO> functions { get; set; }
    }

    public class RoleUpdateRqDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<FunctionListRqDTO> functions { get; set; }
    }

    public class RoleByIdRsDTO
    {
        public RoleDTO Role { get; set; }
        public IEnumerable<RoleFunctionDTO> RoleFunctions { get; set; }
    }
}
