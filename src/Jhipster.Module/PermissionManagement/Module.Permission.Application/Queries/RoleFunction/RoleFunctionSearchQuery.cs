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
    public class RoleFunctionSearchQuery : IRequest<IEnumerable<RoleFunctionDTO>>
    {
        public string? keyword { get; set; }
        public string? roleId { get; set; }
        public bool? status { get; set; }
        public bool isAll { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }


    public class RoleFunctionSearchQueryHandler : IRequestHandler<RoleFunctionSearchQuery, IEnumerable<RoleFunctionDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RoleFunctionSearchQueryHandler> _logger;
        private readonly IRoleFunctionRepository _roleFunctionRepository;

        public RoleFunctionSearchQueryHandler(IRoleFunctionRepository roleFunctionRepository, IMapper mapper, ILogger<RoleFunctionSearchQueryHandler> logger)
        {
            _roleFunctionRepository = roleFunctionRepository ?? throw new ArgumentNullException(nameof(roleFunctionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<RoleFunctionDTO>> Handle(RoleFunctionSearchQuery request, CancellationToken cancellationToken)
        {
            var rolefunctions = await _roleFunctionRepository.SearchRoleFunctions(request.keyword, request.roleId, request.status, request.isAll, request.page, request.pageSize);
            if(rolefunctions == null) Enumerable.Empty<RoleFunctionDTO>();
            return _mapper.Map<IEnumerable<RoleFunctionDTO>>(rolefunctions);
        }
    }

}
