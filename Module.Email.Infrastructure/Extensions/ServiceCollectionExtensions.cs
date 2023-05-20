using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Email.Application.Commands;
using Module.Email.Application.Persistences;
using Module.Email.Domain.Abstractions;
using Module.Email.Infrastructure.Extensions.Persistences.Repositories;
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
            services.AddMediatR(typeof(AddUtmCommand).Assembly);

            //// Đăng kí repository
            services.AddScoped(typeof(IUtmRepository), typeof(UtmRepository));

            return services;
            }
        
    }
}
