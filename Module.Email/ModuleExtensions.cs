using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Email.Domain.Extensions;
using Module.Email.Infrastructure.Extensions;

namespace Module.Email
{
    public static class ModuleExtensions
    {
         
        public static IServiceCollection AddEmailModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore().AddBaseInfrastructure(configuration);


            return services;
        }


    }
}

