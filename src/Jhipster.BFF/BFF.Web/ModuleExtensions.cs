
using BFF.Web.Configurations;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Jhipster.gRPC.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Factor.gRPC.Persistences;
using Module.Factor.Services;
using ProtoBuf.Grpc.ClientFactory;

namespace BFF.Web
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddBFFWebModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddTransient<IMerchantService, MerchantService>();
            services.AddTransient<IAccountService, AccountService>();

            //services.AddTransient<ICartService, CartService>();
            //services.AddTransient<IOrderItemService, OrderItemService>();
            //services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();

            //catalog

            //factor
            services.AddCodeFirstGrpcClient<IMerchantService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            })
           .AddInterceptor<GrpcExceptionInterceptor>();

            //order

         //   services.AddCodeFirstGrpcClient<ICartService>(o =>
         //   {
         //       o.Address = new Uri(configuration.GetConnectionString("AIO"));
         //   })
         //.AddInterceptor<GrpcExceptionInterceptor>();
         //   services.AddCodeFirstGrpcClient<IOrderItemService>(o =>
         //   {
         //       o.Address = new Uri(configuration.GetConnectionString("AIO"));
         //   })
         //.AddInterceptor<GrpcExceptionInterceptor>();
         //   services.AddCodeFirstGrpcClient<IPurchaseOrderService>(o =>
         //   {
         //       o.Address = new Uri(configuration.GetConnectionString("AIO"));
         //   })
         //.AddInterceptor<GrpcExceptionInterceptor>();
            return services;
        }
    }
}
