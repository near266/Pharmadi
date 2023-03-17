// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.LabelCm;
using Module.Catalog.Application.Queries.LabelQ;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Catalog.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILogger<LabelService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public LabelService(ILogger<LabelService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<LabelBaseResponse> Add(LabelAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add Label : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<LabelAddCommand>(request);
                var result = await _mediator.Send(command);
                return new LabelBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd Label", ex);
                return new LabelBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<LabelBaseResponse> Delete(LabelDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete Label : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<LabelDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new LabelBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete Label", ex);
                return new LabelBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<LabelBaseResponse> Update(LabelUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update Label : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<LabelUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new LabelBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update Label", ex);
                return new LabelBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<LabelGetAllAdminResponse>> GetAllAdmin(LabelGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all Label admin : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<LabelGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<LabelGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all Label admin", ex);
                return null;
            }
        }

        public async Task<IEnumerable<LabelSearchResponse>> Search(LabelSearchRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search Label : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<LabelSearchQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<LabelSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search Label", ex);
                return null;
            }
        }

    }
}

