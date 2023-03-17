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
    public class RoleDeleteCommand : IRequest<int>
    {
        [MaxLength(100)]
        public string Id { get; set; }
    }


    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleDeleteCommandHandler> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleDeleteCommandHandler(IMapper mapper, ILogger<RoleDeleteCommandHandler> logger, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.DeleteRole(request.Id);
        }
    }
}
