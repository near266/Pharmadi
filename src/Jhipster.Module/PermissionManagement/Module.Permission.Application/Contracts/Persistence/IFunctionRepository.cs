// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Module.Permission.Application.Dtos;
using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Application.Contracts.Persistence
{
    public interface IFunctionRepository
    {
        Task<Function> GetFunctionById(Guid id);
        Task<IEnumerable<FunctionAllDTO>> SearchFunctions(string keyword, bool? status, bool isAll, Guid? functiontype, int page, int pagesize);
        Task<int> AddFunction(Function function);
        Task<Function> UpdateFunction(Function function);
        Task<int> DeleteFunction(Guid id);
    }
}
