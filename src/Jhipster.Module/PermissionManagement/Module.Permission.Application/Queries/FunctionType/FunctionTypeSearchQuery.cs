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
    public class FunctionTypeSearchQuery : IRequest<IEnumerable<FunctionTypeDTO>>
    {
        public string? keyword { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }


    public class FunctionTypeSearchQueryHandler : IRequestHandler<FunctionTypeSearchQuery, IEnumerable<FunctionTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionTypeSearchQueryHandler> _logger;
        private readonly IFunctionTypeRepository _functionTypeRepository;

        public FunctionTypeSearchQueryHandler(IFunctionTypeRepository functionTypeRepository, IMapper mapper, ILogger<FunctionTypeSearchQueryHandler> logger)
        {
            _functionTypeRepository = functionTypeRepository ?? throw new ArgumentNullException(nameof(functionTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<FunctionTypeDTO>> Handle(FunctionTypeSearchQuery request, CancellationToken cancellationToken)
        {
            var functionTypes = await _functionTypeRepository.SearchFunctionTypes(request.keyword, request.page, request.pageSize);
            if(functionTypes == null) return Enumerable.Empty<FunctionTypeDTO>(); ;
            return _mapper.Map<IEnumerable<FunctionTypeDTO>>(functionTypes);
        }
    }

}
