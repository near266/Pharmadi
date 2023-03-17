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
    public class FunctionUpdateCommand : IRequest<FunctionDTO>
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid FunctionTypeId { get; set; }
        public bool Status { get; set; } = true;
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }


    public class FunctionUpdateCommandHandler : IRequestHandler<FunctionUpdateCommand, FunctionDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionUpdateCommandHandler> _logger;
        private readonly IFunctionRepository _functionRepository;

        public FunctionUpdateCommandHandler(IMapper mapper, ILogger<FunctionUpdateCommandHandler> logger, IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository ?? throw new ArgumentNullException(nameof(functionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<FunctionDTO> Handle(FunctionUpdateCommand request, CancellationToken cancellationToken)
        {
            var function = _mapper.Map<Function>(request);
            var update = await _functionRepository.UpdateFunction(function);
            if (update == null) return null;
            return _mapper.Map<FunctionDTO>(update);
        }
    }
}
