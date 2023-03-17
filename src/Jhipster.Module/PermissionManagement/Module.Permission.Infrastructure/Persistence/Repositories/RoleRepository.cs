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
    public class RoleRepository : IRoleRepository
    {
        private readonly PermissionDbContext _context;
        public RoleRepository(PermissionDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddRole(Role role)
        {
            role.Id = role.Name.ToLowerInvariant();
            role.NormalizedName = role.Name.ToUpperInvariant();
            await _context.Roles.AddAsync(role);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteRole(string id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(i => i.Id == id);
            if(role == null) return 0;
            _context.Roles.Remove(role);
            return await _context.SaveChangesAsync();
        }
        public async Task<Role> GetRoleById(string id)
        {
            return await _context.Roles.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Role>> SearchRoles(string keyword)
        {
            var roleQuery = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                roleQuery = roleQuery.Where(i => i.Name.Contains(keyword) || i.NormalizedName.Contains(keyword));
            }
            var roles = await roleQuery.Include(r => r.RoleFunctions).ThenInclude(f => f.Function).ThenInclude(f=>f.FunctionType).ToListAsync();

            /*var roles = await roleQuery
                .Skip(pagesize * page)
                .Take(pagesize)
                .ToListAsync();*/

            return roles;
        }
        public async Task<Role> UpdateRole(Role role)
        {
            var oldRole = await _context.Roles.FirstOrDefaultAsync(i => i.Id == role.Id);
            if(oldRole == null) return null;
            oldRole.Name = role.Name;
            oldRole.NormalizedName = role.Name.ToUpperInvariant();
            await _context.SaveChangesAsync();
            return oldRole;
        }
    }
}
