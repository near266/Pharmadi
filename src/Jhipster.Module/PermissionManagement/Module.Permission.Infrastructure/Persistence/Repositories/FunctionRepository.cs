// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Microsoft.EntityFrameworkCore;
using Module.Permission.Application.Contracts.Persistence;
using Module.Permission.Application.Dtos;
using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Infrastructure.Persistence.Repositories
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly PermissionDbContext _context;
        public FunctionRepository(PermissionDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddFunction(Function function)
        {
            await _context.Functions.AddAsync(function);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFunction(Guid id)
        {
            var function = await _context.Functions.FirstOrDefaultAsync(i => i.Id == id);
            if (function == null) return 0;
            _context.Functions.Remove(function);
            return await _context.SaveChangesAsync();
        }
        public async Task<Function> GetFunctionById(Guid id)
        {
            return await _context.Functions.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<FunctionAllDTO>> SearchFunctions(string keyword, bool? status, bool isAll, Guid? functiontype, int page, int pagesize)
        {
            var functionQuery = _context.Functions.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                functionQuery = functionQuery.Where(i => i.Name.Contains(keyword) || (!string.IsNullOrEmpty(i.Description) && i.Description.Contains(keyword)));
            }

            if (!isAll)
            {
                functionQuery = functionQuery.Where(i => i.Status == status);
            }

            if(functiontype != null && functiontype != Guid.Empty)
            {
                functionQuery = functionQuery.Where(i => i.FunctionTypeId == functiontype);
            }

            var functions = await functionQuery
                .GroupBy(i => i.FunctionTypeId)
                .Select(i => new {
                    Key = i.Key,
                    Functions = i.ToList()
                })
                .Join(_context.FunctionTypes, i => i.Key, j => j.Id, (i, j) => new FunctionAllDTO
                {
                    Id = j.Id,
                    Name = j.Name,
                    Description = j.Description,
                    Function = j.Function
                })
                .ToListAsync();

            return functions;
        }
        public async Task<Function> UpdateFunction(Function function)
        {
            var oldFunction = await _context.Functions.FirstOrDefaultAsync(i => i.Id == function.Id);
            if (oldFunction == null) return null;

            oldFunction.Name = function.Name;
            oldFunction.Description = function.Description;
            oldFunction.Status = function.Status;
            oldFunction.FunctionTypeId = function.FunctionTypeId;
            oldFunction.LastModifiedBy = function.LastModifiedBy;
            oldFunction.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return oldFunction;
        }
    }
}
