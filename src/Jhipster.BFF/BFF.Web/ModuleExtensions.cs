
using BFF.Web.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BFF.Web
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddBFFWebModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GrpcExceptionInterceptor>();

           
            //services.AddTransient<ICartService, CartService>();
            //services.AddTransient<IOrderItemService, OrderItemService>();
            //services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();

            //catalog

           // //factor
           // services.AddCodeFirstGrpcClient<IMerchantService>(o =>
           // {
           //     o.Address = new Uri(configuration.GetConnectionString("AIO"));
           // })
           //.AddInterceptor<GrpcExceptionInterceptor>();

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
