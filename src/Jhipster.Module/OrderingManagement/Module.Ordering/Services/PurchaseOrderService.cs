// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Factor.Application.Queries.PurchaseOrderQ;
using Module.Ordering.Application.Commands.PurchaseOrderCm;
using Module.Ordering.gRPC.Contracts;
using Module.Ordering.gRPC.Contracts.PagedListC;
using Module.Ordering.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Ordering.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ILogger<PurchaseOrderService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PurchaseOrderService(ILogger<PurchaseOrderService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<PurchaseOrderBaseResponse> Add(PurchaseOrderAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add PurchaseOrder : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<PurchaseOrderAddCommand>(request);
                var result = await _mediator.Send(command);
                return new PurchaseOrderBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd PurchaseOrder", ex);
                return new PurchaseOrderBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<PurchaseOrderBaseResponse> Delete(PurchaseOrderDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete PurchaseOrder : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<PurchaseOrderDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new PurchaseOrderBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete PurchaseOrder", ex);
                return new PurchaseOrderBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PurchaseOrderBaseResponse> Update(PurchaseOrderUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update PurchaseOrder : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<PurchaseOrderUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new PurchaseOrderBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update PurchaseOrder", ex);
                return new PurchaseOrderBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<PurchaseOrderInforAdmin>> GetAllPurchaseOrderByAdmin(PurchaseOrderGetAllAdminRequest request, CallContext context = default)
        { 
            try
            {
                _logger.LogInformation($"GRPC request to PurchaseOrderGetAllAdminRequest : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<PurchaseOrderGetAllByAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<PurchaseOrderInforAdmin>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to PurchaseOrderInforAdmin", ex);
                return null;
            }
        }

        public async Task<PagedListC<PurchaseOrderInforUser>> GetAllPurchaseOrderByUser(PurchaseOrderGetAllUserRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to PurchaseOrderGetAllByUser : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<PurchaseOrderGetAllByUserQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<PurchaseOrderInforUser>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to PurchaseOrderGetAllByUser", ex);
                return null;
            }
        }

        public async Task<PurchaseOrderInforDetail> ViewDetailPurchaseOrder(PurchaseOrderViewDetailRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to ViewDetailPurchaseOrder : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<PurchaseOrderViewDetailQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map< PurchaseOrderInforDetail> (temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to ViewDetailPurchaseOrder", ex);
                return null;
            }
        }

    }
}

