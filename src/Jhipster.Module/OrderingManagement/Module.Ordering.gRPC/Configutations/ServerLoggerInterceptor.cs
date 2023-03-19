// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Module.Ordering.gRPC.Configurations
{
    public class ServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger<ServerLoggerInterceptor> _logger;

        public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger)
        {
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
          TRequest request,
          ServerCallContext context,
          UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogCall<TRequest, TResponse>(MethodType.Unary, context);
            return continuation(request, context);
        }

        private void LogCall<TRequest, TResponse>(MethodType methodType, ServerCallContext context)
          where TRequest : class
          where TResponse : class
        {
            _logger.LogInformation($"Starting call. Type: {methodType}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
        }
    }
}
