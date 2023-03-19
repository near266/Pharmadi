// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Queries.OrderItemQ;
using Module.Ordering.Application.Commands.OrderItemCm;
using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Contracts.PagedListC;
using Module.Ordering.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Ordering.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ILogger<OrderItemService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderItemService(ILogger<OrderItemService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<OrderItemBaseResponse> Add( OrderItemAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add OrderItem : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<OrderItemAddCommand>(request);
                var result = await _mediator.Send(command);
                return new OrderItemBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd OrderItem", ex);
                return new OrderItemBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<OrderItemBaseResponse> Delete(OrderItemDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete OrderItem : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<OrderItemDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new OrderItemBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete OrderItem", ex);
                return new OrderItemBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<OrderItemBaseResponse> Update(OrderItemUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update OrderItem : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<OrderItemUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new OrderItemBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update OrderItem", ex);
                return new OrderItemBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<OrderItemInfor>> ItemsGetAllByOrder(ItemGetAllByOrderRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all OrderItem By Order : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<OrderItemGetAllByUserQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<OrderItemInfor>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all OrderItem By Order", ex);
                return null;
            }
        }

  
    }
}

