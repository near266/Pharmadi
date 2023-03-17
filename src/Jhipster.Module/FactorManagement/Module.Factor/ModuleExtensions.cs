using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Module.Factor.Domain.Extensions;
using Module.Factor.Services;
using Module.Factor.gRPC;
using Module.Factor.Infrastructure.Extensions;

namespace Module.Factor
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddFactorModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);

            services.AddFactorgRPCModule(configuration);

            return services;
        }

        public static IApplicationBuilder AddFactorgRPCModule(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchantService>();
            });

            return app;
        }
    }
}
