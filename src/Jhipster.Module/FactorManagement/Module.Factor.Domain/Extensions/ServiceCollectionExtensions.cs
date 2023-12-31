﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Module.Factor.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
