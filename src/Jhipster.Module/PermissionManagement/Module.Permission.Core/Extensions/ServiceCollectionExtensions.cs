using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module.Permission.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPermissionCore(this IServiceCollection services)
        {
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
