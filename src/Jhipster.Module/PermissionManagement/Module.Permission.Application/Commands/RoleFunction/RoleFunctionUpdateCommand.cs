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
    public class RoleFunctionUpdateCommand : IRequest<int>
    {
        public string roleId { get; set; }
        public List<FunctionListRqDTO>? functions { get; set; }
        [JsonIgnore]
        public string? ModifiedBy { get; set; }
    }


    public class RoleFunctionUpdateCommandHandler : IRequestHandler<RoleFunctionUpdateCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleFunctionUpdateCommandHandler> _logger;
        private readonly IRoleFunctionRepository _roleFunctionRepository;

        public RoleFunctionUpdateCommandHandler(IMapper mapper, ILogger<RoleFunctionUpdateCommandHandler> logger, IRoleFunctionRepository roleFunctionRepository)
        {
            _roleFunctionRepository = roleFunctionRepository ?? throw new ArgumentNullException(nameof(roleFunctionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(RoleFunctionUpdateCommand request, CancellationToken cancellationToken)
        {
            // Deactive role function
            var listDe = request.functions.Where(i => i.Id != Guid.Empty && i.Status == false).Select(i => i.Id).ToList();
            var deact = await _roleFunctionRepository.DeactiveRoleFunction(request.roleId, listDe, request.ModifiedBy);

            // Update status
            var listUp = request.functions.Where(i => i.Id != Guid.Empty && i.Status == true).Select(i => i.Id).ToList();
            var update = await _roleFunctionRepository.UpdateRoleFunction(request.roleId, listUp, request.ModifiedBy);

            return update;
        }
    }
}
