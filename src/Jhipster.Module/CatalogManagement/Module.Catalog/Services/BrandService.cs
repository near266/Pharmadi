// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.BrandCm;
using Module.Catalog.Application.Queries.BrandQ;
using Module.Catalog.Application.Queries.GroupBrandQ;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Catalog.Services
{
    public class BrandService : IBrandService
    {
        private readonly ILogger<BrandService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BrandService(ILogger<BrandService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<BrandBaseResponse> Add(BrandAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add Brand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<BrandAddCommand>(request);
                var result = await _mediator.Send(command);
                return new BrandBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd Brand", ex);
                return new BrandBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<BrandBaseResponse> Delete(BrandDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete Brand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<BrandDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new BrandBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete Brand", ex);
                return new BrandBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<BrandBaseResponse> Update(BrandUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update Brand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<BrandUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new BrandBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update Brand", ex);
                return new BrandBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<BrandGetAllAdminResponse>> GetAllAdmin(BrandGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all Brand admin : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<BrandGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<BrandGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all Brand admin", ex);
                return null;
            }
        }

        public async Task<IEnumerable<BrandSearchResponse>> Search(BrandSearchRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search Brand : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<BrandSearchQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<BrandSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search Brand", ex);
                return null;
            }
        }

    }
}

