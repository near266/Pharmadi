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
    public class RoleUpdateCommand : IRequest<RoleDTO>
    {
        public string Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }


    public class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommand, RoleDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleUpdateCommandHandler> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleUpdateCommandHandler(IMapper mapper, ILogger<RoleUpdateCommandHandler> logger, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<RoleDTO> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request);
            var update = await _roleRepository.UpdateRole(role);
            if(update == null) return null;
            return _mapper.Map<RoleDTO>(update);
        }
    }
}
