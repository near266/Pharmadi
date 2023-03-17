
using Microsoft.Extensions.DependencyInjection;

namespace Module.Permission.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPermissionApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
