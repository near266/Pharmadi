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
    public class FunctionTypeUpdateCommand : IRequest<FunctionTypeDTO>
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }


    public class FunctionTypeUpdateCommandHandler : IRequestHandler<FunctionTypeUpdateCommand, FunctionTypeDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionTypeUpdateCommandHandler> _logger;
        private readonly IFunctionTypeRepository _functionTypeRepository;

        public FunctionTypeUpdateCommandHandler(IMapper mapper, ILogger<FunctionTypeUpdateCommandHandler> logger, IFunctionTypeRepository functionTypeRepository)
        {
            _functionTypeRepository = functionTypeRepository ?? throw new ArgumentNullException(nameof(functionTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<FunctionTypeDTO> Handle(FunctionTypeUpdateCommand request, CancellationToken cancellationToken)
        {
            var functionType = _mapper.Map<FunctionType>(request);
            var update = await _functionTypeRepository.UpdateFunctionType(functionType);
            if(update == null) return null;
            return _mapper.Map<FunctionTypeDTO>(update);
        }
    }
}
