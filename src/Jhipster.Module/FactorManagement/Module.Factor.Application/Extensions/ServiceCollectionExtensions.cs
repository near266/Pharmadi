
using Microsoft.Extensions.DependencyInjection;

namespace Module.Factor.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
