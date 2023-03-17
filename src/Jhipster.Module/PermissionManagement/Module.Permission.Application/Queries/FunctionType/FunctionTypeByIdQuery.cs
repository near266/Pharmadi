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
    public class FunctionTypeByIdQuery : IRequest<FunctionTypeDTO>
    {
        public Guid Id { get; set; }
    }


    public class FunctionTypeByIdQueryHandler : IRequestHandler<FunctionTypeByIdQuery, FunctionTypeDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FunctionTypeByIdQueryHandler> _logger;
        private readonly IFunctionTypeRepository _functionTypeRepository;

        public FunctionTypeByIdQueryHandler(IFunctionTypeRepository functionTypeRepository, IMapper mapper, ILogger<FunctionTypeByIdQueryHandler> logger)
        {
            _functionTypeRepository = functionTypeRepository ?? throw new ArgumentNullException(nameof(functionTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<FunctionTypeDTO> Handle(FunctionTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var functionType = await _functionTypeRepository.GetFunctionTypeById(request.Id);
            if(functionType == null) return null;
            return _mapper.Map<FunctionTypeDTO>(functionType);
        }
    }

}
