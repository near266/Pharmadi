// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Module.Permission.Application.Contracts.Persistence;
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
    public class RoleAddCommand : IRequest<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }


    public class RoleAddCommandHandler : IRequestHandler<RoleAddCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleAddCommandHandler> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleAddCommandHandler(IMapper mapper, ILogger<RoleAddCommandHandler> logger, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(RoleAddCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request);
            return await _roleRepository.AddRole(role);
        }
    }
}
