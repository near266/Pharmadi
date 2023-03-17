using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Permission.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Module.Permission.Infrastructure.Extensions;

namespace Module.Permission
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddPermissionModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPermissionCore()
                .AddPermissionInfrastructure(configuration);

            return services;
        }
    }
}
