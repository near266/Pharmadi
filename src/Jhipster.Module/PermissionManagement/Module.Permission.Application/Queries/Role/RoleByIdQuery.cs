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
    public class RoleByIdQuery : IRequest<RoleDTO>
    {
        public string Id { get; set; }
    }


    public class RoleByIdQueryHandler : IRequestHandler<RoleByIdQuery, RoleDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleByIdQueryHandler> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleByIdQueryHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<RoleDTO> Handle(RoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleById(request.Id);
            if(role == null) return null;
            return _mapper.Map<RoleDTO>(role);
        }
    }

}
