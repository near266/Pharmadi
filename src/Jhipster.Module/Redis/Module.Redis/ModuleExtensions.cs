using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Module.Redis
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddRedisModule(this IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Redis:RedisEnabled"])){
                services.AddStackExchangeRedisCache(options => {
                    options.Configuration = configuration["Redis:Configuration"];
                    options.InstanceName = configuration["Redis:InstanceName"];
                });
            }

            return services;
        }
    }
}
