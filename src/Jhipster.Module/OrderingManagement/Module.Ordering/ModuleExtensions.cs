using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Ordering.Domain.Extensions;
using Module.Ordering.Infrastructure.Extensions;

namespace Module.Ordering
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);

            return services;
        }

    }
}
