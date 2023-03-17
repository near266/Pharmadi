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
    public class FunctionByIdQuery : IRequest<FunctionDTO>
    {
        public Guid Id { get; set; }
    }


    public class FunctionByIdQueryHandler : IRequestHandler<FunctionByIdQuery, FunctionDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionByIdQueryHandler> _logger;
        private readonly IFunctionRepository _functionRepository;

        public FunctionByIdQueryHandler(IFunctionRepository functionRepository, IMapper mapper, ILogger<FunctionByIdQueryHandler> logger)
        {
            _functionRepository = functionRepository ?? throw new ArgumentNullException(nameof(functionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<FunctionDTO> Handle(FunctionByIdQuery request, CancellationToken cancellationToken)
        {
            var function = await _functionRepository.GetFunctionById(request.Id);
            if(function == null) return null;
            return _mapper.Map<FunctionDTO>(function);
        }
    }

}
