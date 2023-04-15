// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Microsoft.EntityFrameworkCore;
using Module.Permission.Application.Contracts.Persistence;
using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Infrastructure.Persistence.Repositories
{
    public class RoleFunctionRepository : IRoleFunctionRepository
    {
        private readonly PermissionDbContext _context;
        public RoleFunctionRepository(PermissionDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddRoleFunction(string roleId, List<Guid> list, string createdBy)
        {
            if (list == null || list.Count <= 0) return 0;
            var listFucntion = list.Where(i => i != Guid.Empty).Select(i => new RoleFunction
            {
                RoleId = roleId,
                FunctionId = i,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy,
                Status = true
            }).ToList();

            await _context.RoleFunctions.AddRangeAsync(listFucntion);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeactiveRoleFunction(string roleId, List<Guid> list, string modifiedBy)
        {
            if (list == null || list.Count <= 0) return 0;
            foreach(var fid in list.Where(i => i != Guid.Empty))
            {
                var roleFunction = await _context.RoleFunctions.FirstOrDefaultAsync(j => j.RoleId == roleId && j.FunctionId == fid);
                if (roleFunction == null) continue;
                roleFunction.Status = false;
                roleFunction.LastModifiedDate = DateTime.Now;
                roleFunction.LastModifiedBy = modifiedBy;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<string> GetRoleFunctions(string roles)
        {
            if(string.IsNullOrEmpty(roles)) return string.Empty;
            var function = string.Empty;
            var role = roles.Split(",");
            foreach(var r in role)
            {
                var list = await _context.RoleFunctions.Where(i => i.RoleId.Equals(r.ToLower())).Join(_context.Functions, i => i.FunctionId, j => j.Id, (i ,j) => j.Name).ToArrayAsync();
                if (list != null && list.Length > 0)
                    function = function + string.Join(",", list);
            }

            return function;
        }

        public async Task<IEnumerable<RoleFunction>> SearchRoleFunctions(string? keyword, string? roleId, bool? status, bool isAll, int page, int pagesize)
        {
            var roleFunctionQuery = _context.RoleFunctions.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                roleFunctionQuery = roleFunctionQuery.Where(i => i.FunctionId.ToString().Contains(keyword) || i.RoleId.Contains(keyword) || (!string.IsNullOrEmpty(i.CreatedBy) && i.CreatedBy.Contains(keyword)));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                roleFunctionQuery = roleFunctionQuery.Where(i => i.RoleId.Equals(roleId));
            }

            if (!isAll)
            {
                roleFunctionQuery = roleFunctionQuery.Where(i => i.Status == status);
            }

            var roleFunction = await roleFunctionQuery
                .Skip(pagesize * (page-1))
                .Take(pagesize)
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();

            return roleFunction;
        }
        public async Task<int> UpdateRoleFunction(string roleId, List<Guid> list, string modifiedBy)
        {
            if (list == null || list.Count <= 0) return 0;
            foreach (var fid in list.Where(i => i != Guid.Empty))
            {
                var roleFunction = await _context.RoleFunctions.FirstOrDefaultAsync(j => j.RoleId == roleId && j.FunctionId == fid);
                if (roleFunction == null)
                {
                    _context.RoleFunctions.Add(new RoleFunction
                    {
                        RoleId = roleId,
                        FunctionId = fid,
                        CreatedDate = DateTime.Now,
                        CreatedBy = modifiedBy,
                        Status = true
                    });
                }
                else
                {
                    roleFunction.Status = true;
                    roleFunction.LastModifiedDate = DateTime.Now;
                    roleFunction.LastModifiedBy = modifiedBy;
                }
            }

            return await _context.SaveChangesAsync();
        }
    }
}
