using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Module.Catalog.Domain.Extensions;
using Module.Catalog.Services;
using Module.Catalog.Infrastructure.Extensions;
using Module.Catalog.gRPC;

namespace Module.Catalog
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);

            services.AddCataloggRPCModule(configuration);

            return services;
        }

        public static IApplicationBuilder AddCatgaloggRPCModule(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CategoryService>();
            });

            return app;
        }
    }
}
