
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Module.Permission.Application.Contracts.Persistence;
using Module.Permission.Application.Dtos;

namespace Module.Permission.Application.Queries
{
    public class RoleSearchQuery : IRequest<IEnumerable<RoleDTO>>
    {
        public string? keyword { get; set; }
        /*public int page { get; set; }
        public int pageSize { get; set; }*/
    }


    public class RoleSearchQueryHandler : IRequestHandler<RoleSearchQuery, IEnumerable<RoleDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleSearchQueryHandler> _logger;
        private readonly IRoleRepository _roleRepository;

        public RoleSearchQueryHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<RoleSearchQueryHandler> logger)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<RoleDTO>> Handle(RoleSearchQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.SearchRoles(request.keyword);
            if(roles == null) return Enumerable.Empty<RoleDTO>();
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
    }

}
