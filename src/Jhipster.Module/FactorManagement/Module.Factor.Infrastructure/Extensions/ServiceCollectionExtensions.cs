
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Factor.Application.Commands.MerchantCm;
using Module.Factor.Application.Persistences;
using Module.Factor.Domain.Abstractions;
using Module.Factor.Infrastructure.Persistence.Repositories;
using Module.Factor.Infrastructure.Persistences;

namespace Module.Factor.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<FactorDbContext>(config)
                .AddScoped<IFactorDbContext>(provider => provider.GetService<FactorDbContext>());
            // Đăng kí mediatR
            services.AddMediatR(typeof(MerchantAddCommand).Assembly);

            //// Đăng kí repository
            services.AddScoped(typeof(IMerchantRepository), typeof(MerchantRepository));
            return services;
        }
    }
}
