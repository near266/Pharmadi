
using Microsoft.Extensions.DependencyInjection;

namespace Module.Catalog.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
