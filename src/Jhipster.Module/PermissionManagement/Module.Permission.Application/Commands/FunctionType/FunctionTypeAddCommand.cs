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
    public class FunctionTypeAddCommand : IRequest<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }


    public class FunctionTypeAddCommandHandler : IRequestHandler<FunctionTypeAddCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionTypeAddCommandHandler> _logger;
        private readonly IFunctionTypeRepository _functionTypeRepository;

        public FunctionTypeAddCommandHandler(IMapper mapper, ILogger<FunctionTypeAddCommandHandler> logger, IFunctionTypeRepository functionTypeRepository)
        {
            _functionTypeRepository = functionTypeRepository ?? throw new ArgumentNullException(nameof(functionTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(FunctionTypeAddCommand request, CancellationToken cancellationToken)
        {
            return await _functionTypeRepository.AddFunctionType(_mapper.Map<FunctionType>(request));
        }
    }
}
