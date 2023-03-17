// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module.Catalog.Application.Commands.ProductCm;
using Module.Catalog.Application.Queries.ProductQ;
using Module.Catalog.gRPC.Contracts;
using Module.Catalog.gRPC.Contracts.PagedListC;
using Module.Catalog.gRPC.Persistences;
using Newtonsoft.Json;
using ProtoBuf.Grpc;

namespace Module.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductService(ILogger<ProductService> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mediator = serviceProvider.GetService<IMediator>();
            _mapper = mapper;

        }
        public async Task<ProductBaseResponse> Add(ProductAddRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to add Product : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ProductAddCommand>(request);
                var result = await _mediator.Send(command);
                return new ProductBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error aadd Product", ex);
                return new ProductBaseResponse
                {
                    message = -1
                };
            }
        }
        public async Task<ProductBaseResponse> Delete(ProductDeleteRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to delete Product : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ProductDeleteCommand>(request);
                var result = await _mediator.Send(command);
                return new ProductBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error delete Product", ex);
                return new ProductBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<ProductBaseResponse> Update(ProductUpdateRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to update Product : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ProductUpdateCommand>(request);
                var result = await _mediator.Send(command);
                return new ProductBaseResponse
                {
                    message = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error update Product", ex);
                return new ProductBaseResponse
                {
                    message = -1
                };
            }
        }

        public async Task<PagedListC<ProductGetAllAdminResponse>> GetAllAdmin(ProductGetAllAdminRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to get all Product admin : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ProductGetAllAdminQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<ProductGetAllAdminResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to get all Product admin", ex);
                return null;
            }
        }

        public async Task<PagedListC<ProductInforSearchResponse>> Search(ProductSearchRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search Product : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<SearchProductQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<PagedListC<ProductInforSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search Product", ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProductInforSearchResponse>> ViewProductForU(ProductSearchListRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to search for product list for u : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ViewProductForUQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<ProductInforSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to search for product list for u ", ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProductInforSearchResponse>> ViewProductBestSale(ProductSearchListRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to ViewProductBestSale : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ViewProductBestSaleQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<ProductInforSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to ViewProductBestSale ", ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProductInforSearchResponse>> ViewProductNew(ProductSearchListRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to ViewProductNew : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ViewProductNewQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<ProductInforSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to ViewProductNew ", ex);
                return null;
            }
        }

        public async Task<IEnumerable<ProductInforSearchResponse>> ViewProductPromotion(ProductSearchListRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to ViewProductPromotion : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ViewProductPromotionQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<IEnumerable<ProductInforSearchResponse>>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to ViewProductPromotion ", ex);
                return null;
            }
        }

        public async Task<ProductViewDetailResponse> ViewDetail(ProductViewDetailRequest request, CallContext context = default)
        {
            try
            {
                _logger.LogInformation($"GRPC request to ViewDetail product : {JsonConvert.SerializeObject(request)}");
                var command = _mapper.Map<ProductViewDetailQuery>(request);
                var temp = await _mediator.Send(command);
                var result = _mapper.Map<ProductViewDetailResponse>(temp);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to ViewDetail product ", ex);
                return null;
            }
        }
    }
}

