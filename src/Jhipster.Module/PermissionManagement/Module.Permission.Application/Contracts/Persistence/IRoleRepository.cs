// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Application.Contracts.Persistence
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleById(string id);
        Task<IEnumerable<Role>> SearchRoles(string keyword);
        Task<int> AddRole(Role role);
        Task<int> DeleteRole(string id);
        Task<Role> UpdateRole(Role role);
    }
}
