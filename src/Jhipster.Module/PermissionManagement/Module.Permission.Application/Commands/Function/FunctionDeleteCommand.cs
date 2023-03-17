﻿// Licensed to the .NET Foundation under one or more agreements.
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
    public class FunctionDeleteCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }


    public class FunctionDeleteCommandHandler : IRequestHandler<FunctionDeleteCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionDeleteCommandHandler> _logger;
        private readonly IFunctionRepository _functionRepository;

        public FunctionDeleteCommandHandler(IMapper mapper, ILogger<FunctionDeleteCommandHandler> logger, IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository ?? throw new ArgumentNullException(nameof(functionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<int> Handle(FunctionDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _functionRepository.DeleteFunction(request.Id);
        }
    }
}
