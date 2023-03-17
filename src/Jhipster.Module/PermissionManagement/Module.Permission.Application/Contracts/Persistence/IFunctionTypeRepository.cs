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
    public interface IFunctionTypeRepository
    {
        Task<FunctionType> GetFunctionTypeById(Guid id);
        Task<IEnumerable<FunctionType>> SearchFunctionTypes(string keyword, int page, int pagesize);
        Task<int> AddFunctionType(FunctionType functionType);
        Task<FunctionType> UpdateFunctionType(FunctionType functionType);
        Task<int> DeleteFunctionType(Guid id);
    }
}
