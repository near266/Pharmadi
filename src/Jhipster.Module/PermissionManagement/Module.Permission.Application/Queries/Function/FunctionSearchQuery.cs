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
    public class FunctionSearchQuery : IRequest<IEnumerable<FunctionAllDTO>>
    {
        public string? keyword { get; set; }
        public Guid? functionTypeId { get; set; }
        public bool? status { get; set; }
        public bool isAll { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }


    public class FunctionSearchQueryHandler : IRequestHandler<FunctionSearchQuery, IEnumerable<FunctionAllDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionSearchQueryHandler> _logger;
        private readonly IFunctionRepository _functionRepository;

        public FunctionSearchQueryHandler(IFunctionRepository functionRepository, IMapper mapper, ILogger<FunctionSearchQueryHandler> logger)
        {
            _functionRepository = functionRepository ?? throw new ArgumentNullException(nameof(functionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<FunctionAllDTO>> Handle(FunctionSearchQuery request, CancellationToken cancellationToken)
        {
            var functions = await _functionRepository.SearchFunctions(request.keyword, request.status, request.isAll, request.functionTypeId, request.page, request.pageSize);
            if(functions == null) return Enumerable.Empty<FunctionAllDTO>();
            return functions;
        }
    }

}
