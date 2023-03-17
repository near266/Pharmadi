using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Module.Permission.Application.Contracts.Persistence;
using Module.Permission.Application.Dtos;

namespace Module.Permission.Application.Queries
{
    public class RoleFunctionGetQuery : IRequest<string>
    {
        public string? roles { get; set; }
    }


    public class RoleFunctionGetQueryHandler : IRequestHandler<RoleFunctionGetQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleFunctionGetQueryHandler> _logger;
        private readonly IRoleFunctionRepository _roleFunctionRepository;

        public RoleFunctionGetQueryHandler(IRoleFunctionRepository roleFunctionRepository, IMapper mapper, ILogger<RoleFunctionGetQueryHandler> logger)
        {
            _roleFunctionRepository = roleFunctionRepository ?? throw new ArgumentNullException(nameof(roleFunctionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<string> Handle(RoleFunctionGetQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.roles)) return string.Empty;
            var rolefunctions = await _roleFunctionRepository.GetRoleFunctions(request.roles);
            return rolefunctions;
        }
    }

}
