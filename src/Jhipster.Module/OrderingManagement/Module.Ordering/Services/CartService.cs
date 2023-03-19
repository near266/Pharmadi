// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Queries.CartQ;
using Module.Ordering.Application.Commands.CartCm;
using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Contracts.PagedListC;
using Module.Ordering.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Ordering.Services
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CartService(ILogger<CartService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<CartBaseResponse> Add(CartAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add Cart : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CartAddCommand>(request);
                var result = await _mediator.Send(command);
                return new CartBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd Cart", ex);
                return new CartBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<CartBaseResponse> Delete(CartDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete Cart : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CartDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new CartBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete Cart", ex);
                return new CartBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<CartBaseResponse> Update(CartUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update Cart : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CartUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new CartBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update Cart", ex);
                return new CartBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<CartInfor>> GetAllCartByUser(CartGetAllByUserRequest request, CallContext context = default)
        { 
            try
            {
                _logger.LogInformation($"GRPC request to getallcartbyuser : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<CartGetAllByUserQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<CartInfor>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to getallcartbyuser", ex);
                return null;
            }
        }

    }
}

