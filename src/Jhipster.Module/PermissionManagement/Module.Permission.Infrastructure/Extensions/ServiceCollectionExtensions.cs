
using Jhipster.Infrastructure.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Module.Permission.Infrastructure.Persistence;
using Module.Permission.Core.Abstractions;
using Module.Permission.Application.Contracts.Persistence;
using Module.Permission.Infrastructure.Persistence.Repositories;
using Module.Permission.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Module.Permission.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPermissionInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<PermissionDbContext>(config)
                .AddScoped<IPermissionDbContext>(provider => provider.GetService<PermissionDbContext>());

            // Đăng kí automapper

            // Đăng kí mediatR
            services.AddMediatR(typeof(RoleSearchQuery).Assembly);

            // Đăng kí repository
            services.AddScoped(typeof(IFunctionRepository), typeof(FunctionRepository));
            services.AddScoped(typeof(IFunctionTypeRepository), typeof(FunctionTypeRepository));
            services.AddScoped(typeof(IRoleFunctionRepository), typeof(RoleFunctionRepository));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));

            //Đăng kí service
            return services;
        }
    }
}
