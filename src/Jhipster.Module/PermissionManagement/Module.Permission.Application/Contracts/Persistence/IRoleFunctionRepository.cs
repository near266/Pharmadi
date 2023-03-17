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
    public interface IRoleFunctionRepository
    {
        Task<IEnumerable<RoleFunction>> SearchRoleFunctions(string? keyword, string? roleId, bool? status, bool isAll,int page, int pagesize);
        Task<int> AddRoleFunction(string roleId, List<Guid> list, string createdBy);
        Task<int> UpdateRoleFunction(string roleId, List<Guid> list, string modifiedBy);
        Task<int> DeactiveRoleFunction(string roleId, List<Guid> list, string modifiedBy);
        Task<string> GetRoleFunctions(string roles);
    }
}
