// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Module.Permission.Application.Contracts.Persistence;
using Module.Permission.Application.Dtos;
using Module.Permission.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Module.Permission.Application.Commands
{
    public class RoleFunctionAddCommand : IRequest<int>
    {
        public string roleId { get; set; }
        public List<FunctionListRqDTO>? functions { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }


    public class RoleFunctionAddCommandHandler : IRequestHandler<RoleFunctionAddCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleFunctionAddCommandHandler> _logger;
        private readonly IRoleFunctionRepository _roleFunctionRepository;

        public RoleFunctionAddCommandHandler(IMapper mapper, ILogger<RoleFunctionAddCommandHandler> logger, IRoleFunctionRepository roleFunctionRepository)
        {
            _roleFunctionRepository = roleFunctionRepository ?? throw new ArgumentNullException(nameof(roleFunctionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(RoleFunctionAddCommand request, CancellationToken cancellationToken)
        {
            var list = request.functions.Where(i => i.Id != Guid.Empty && i.Status == true).Select(i => i.Id).ToList();
            return await _roleFunctionRepository.AddRoleFunction(request.roleId, list, request.CreatedBy);
        }
    }
}
