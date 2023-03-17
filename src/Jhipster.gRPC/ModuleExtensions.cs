using System.IO.Compression;
using Jhipster.gRPC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

namespace Jhipster.gRPC
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddIdentitygRPCModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCodeFirstGrpc(c => {
                c.ResponseCompressionLevel = CompressionLevel.Optimal;
            });

            /*services.AddGrpc(options =>
            {
                options.Interceptors.Add<ServerLoggerInterceptor>();
            });*/

            return services;
        }

        public static IApplicationBuilder AddIdentityAppModule(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AccountService>();
            });

            return app;
        }
    }
}
