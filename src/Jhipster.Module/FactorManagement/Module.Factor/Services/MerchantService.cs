// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Application.Queries.MerchantQ;
using Module.Factor.gRPC.Contracts;
using Module.Factor.gRPC.Contracts.PagedListC;
using Module.Factor.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Factor.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly ILogger<MerchantService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public MerchantService(ILogger<MerchantService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<MerchantBaseResponse> Add(MerchantAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"REST request Add : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<MerchantAddCommand>(request);
                var result = await _mediator.Send(command);
                return new MerchantBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd merchant", ex);
                return new MerchantBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<MerchantBaseResponse> Update(MerchantUpdateRequest request, CallContext context = default)
        {
            try
            {
                var command = _mapper.Map<MerchantUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new MerchantBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update merchant", ex);
                return new MerchantBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<MerchantBaseResponse> Delete(MerchantDeleteRequest request, CallContext context = default)
        {
            try
            {
                var command = _mapper.Map<MerchantDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new MerchantBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete merchant", ex);
                return new MerchantBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<MerchantGetAllAdminResponse>> GetAllAdmin(MerchantGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                var command = _mapper.Map<MerchantGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<MerchantGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error GetAllAdmin merchant", ex);
                return null;
            }
        }

        public async Task<MerchantInforResponse> ViewDetail(MerchantViewDetailRequest request, CallContext context = default)
        {
            try
            {
                var command = _mapper.Map<MerchantViewDetailQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<MerchantInforResponse>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error view detail merchant", ex);
                return null;
            }
        }
    }
}

