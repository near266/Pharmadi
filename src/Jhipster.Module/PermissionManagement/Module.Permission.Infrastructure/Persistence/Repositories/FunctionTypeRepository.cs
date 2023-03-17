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
    public class FunctionTypeRepository : IFunctionTypeRepository
    {
        private readonly PermissionDbContext _context;
        public FunctionTypeRepository(PermissionDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddFunctionType(FunctionType functionType)
        {
            await _context.FunctionTypes.AddAsync(functionType);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFunctionType(Guid id)
        {
            var functionType = await _context.FunctionTypes.FirstOrDefaultAsync(i => i.Id == id);
            if(functionType == null) return 0;
            _context.FunctionTypes.Remove(functionType);
            return await _context.SaveChangesAsync();
        }
        public async Task<FunctionType> GetFunctionTypeById(Guid id)
        {
            return await _context.FunctionTypes.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<FunctionType>> SearchFunctionTypes(string keyword, int page, int pagesize)
        {
            var functionTypeQuery = _context.FunctionTypes.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                functionTypeQuery = functionTypeQuery.Where(i => i.Name.Contains(keyword) || (!string.IsNullOrEmpty(i.Description) && i.Description.Contains(keyword)));
            }

            var functionTypes = await functionTypeQuery
                .Skip(pagesize * page)
                .Take(pagesize)
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();

            return functionTypes;
        }
        public async Task<FunctionType> UpdateFunctionType(FunctionType functionType)
        {
            var oldFunctionType = await _context.FunctionTypes.FirstOrDefaultAsync(i => i.Id == functionType.Id);
            if (oldFunctionType == null) return null;

            oldFunctionType.Name = functionType.Name;
            oldFunctionType.Description = functionType.Description;
            oldFunctionType.LastModifiedBy = functionType.LastModifiedBy;
            oldFunctionType.LastModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return oldFunctionType;
        }
    }
}
