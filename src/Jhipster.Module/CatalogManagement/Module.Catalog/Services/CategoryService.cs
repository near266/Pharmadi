// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.CategoryCm;
using Module.Catalog.Application.Queries.CategoryQ;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CategoryService(ILogger<CategoryService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<CategoryBaseResponse> Add(CategoryAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add category : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CategoryAddCommand>(request);
                var result = await _mediator.Send(command);
                return new CategoryBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd category", ex);
                return new CategoryBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<CategoryBaseResponse> Delete(CategoryDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete category : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CategoryDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new CategoryBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete category", ex);
                return new CategoryBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<CategoryBaseResponse> Update(CategoryUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update category : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CategoryUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new CategoryBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update category", ex);
                return new CategoryBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<CategoryGetAllAdminResponse>> GetAllAdmin(CategoryGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all category admin : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CategoryGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<CategoryGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all category admin", ex);
                return null;
            }
        }

        public async Task<IEnumerable<CategorySearchResponse>> Search(CategorySearchRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search category : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CategorySearchQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<CategorySearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search category", ex);
                return null;
            }
        }

    }
}

