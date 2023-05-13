using Jhipster.Infrastructure.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Email.Domain.Abstractions;
using Module.Email.Infrastructure.Persistences;
using Module.Factor.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Email.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
       
            public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
            {
                services
                    .AddDatabaseContext<EmailDbContext>(config)
                    .AddScoped<IEmailDbContext>(provider => provider.GetService<EmailDbContext>());
                // Đăng kí mediatR
                

                //// Đăng kí repository
               
                return services;
            }
        
    }
}
