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
    public class FunctionTypeDeleteCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }


    public class FunctionTypeDeleteCommandHandler : IRequestHandler<FunctionTypeDeleteCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionTypeDeleteCommandHandler> _logger;
        private readonly IFunctionTypeRepository _functionTypeRepository;

        public FunctionTypeDeleteCommandHandler(IMapper mapper, ILogger<FunctionTypeDeleteCommandHandler> logger, IFunctionTypeRepository functionTypeRepository)
        {
            _functionTypeRepository = functionTypeRepository ?? throw new ArgumentNullException(nameof(functionTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(FunctionTypeDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _functionTypeRepository.DeleteFunctionType(request.Id);
        }
    }
}
