// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.TagCm;
using Module.Catalog.Application.Queries.TagQ;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Catalog.Services
{
    public class TagService : ITagService
    {
        private readonly ILogger<TagService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TagService(ILogger<TagService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<TagBaseResponse> Add(TagAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add Tag : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<TagAddCommand>(request);
                var result = await _mediator.Send(command);
                return new TagBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd Tag", ex);
                return new TagBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<TagBaseResponse> Delete(TagDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete Tag : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<TagDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new TagBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete Tag", ex);
                return new TagBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<TagBaseResponse> Update(TagUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update Tag : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<TagUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new TagBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update Tag", ex);
                return new TagBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<TagGetAllAdminResponse>> GetAllAdmin(TagGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all Tag admin : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<TagGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<TagGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all Tag admin", ex);
                return null;
            }
        }

        public async Task<IEnumerable<TagSearchResponse>> Search(TagSearchRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search Tag : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<TagSearchQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<TagSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search Tag", ex);
                return null;
            }
        }

    }
}

