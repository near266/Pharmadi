// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.GroupBrandCm;
using Module.Catalog.Application.Queries.GroupBrandQ;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Catalog.Services
{
    public class GroupBrandService : IGroupBrandService
    {
        private readonly ILogger<GroupBrandService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GroupBrandService(ILogger<GroupBrandService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<GroupBrandBaseResponse> Add(GroupBrandAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add GroupBrand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<GroupBrandAddCommand>(request);
                var result = await _mediator.Send(command);
                return new GroupBrandBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd GroupBrand", ex);
                return new GroupBrandBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<GroupBrandBaseResponse> Delete(GroupBrandDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete GroupBrand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<GroupBrandDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new GroupBrandBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete GroupBrand", ex);
                return new GroupBrandBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<GroupBrandBaseResponse> Update(GroupBrandUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update GroupBrand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<GroupBrandUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new GroupBrandBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update GroupBrand", ex);
                return new GroupBrandBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<GroupBrandGetAllAdminResponse>> GetAllAdmin(GroupBrandGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all GroupBrand admin : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<GroupBrandGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<GroupBrandGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all GroupBrand admin", ex);
                return null;
            }
        }

        public async Task<IEnumerable<GroupBrandSearchResponse>> Search(GroupBrandSearchRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search GroupBrand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<GroupBrandSearchQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<GroupBrandSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search GroupBrand", ex);
                return null;
            }
        }

    }
}

