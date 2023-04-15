using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Factor.Domain.Extensions;
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


            return services;
        }

      
    }
}
