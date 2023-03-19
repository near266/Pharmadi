using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Ordering.gRPC.Configurations;
using ProtoBuf.Grpc.Server;

namespace Module.Ordering.gRPC
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddOrderinggRPCModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCodeFirstGrpc(c => {
                c.ResponseCompressionLevel = CompressionLevel.Optimal;
            });

            services.AddServiceModelGrpc();

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<ServerLoggerInterceptor>();
            });

            return services;
        }
    }
}
