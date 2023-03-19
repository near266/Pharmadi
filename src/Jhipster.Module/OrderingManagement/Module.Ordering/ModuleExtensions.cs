using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Module.Ordering.Services;
using Module.Ordering.Domain.Extensions;
using Module.Ordering.Infrastructure.Extensions;
using Module.Ordering.gRPC;

namespace Module.Ordering
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);

            services.AddOrderinggRPCModule(configuration);

            return services;
        }

        public static IApplicationBuilder AddCatgaloggRPCModule(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CartService>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<OrderItemService>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<PurchaseOrderService>();
            });

            return app;
        }
    }
}
