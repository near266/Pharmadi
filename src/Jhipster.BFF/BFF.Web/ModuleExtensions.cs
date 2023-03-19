
using BFF.Web.Configurations;
using Jhipster.gRPC.Contracts.Shared.Identity;
using Jhipster.gRPC.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.gRPC.Persistences;
using Module.Catalog.Services;
using Module.Factor.gRPC.Persistences;
using Module.Factor.Services;
using Module.Ordering.gRPC.Persistences;
using Module.Ordering.Services;
using ProtoBuf.Grpc.ClientFactory;

namespace BFF.Web
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddBFFWebModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GrpcExceptionInterceptor>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IGroupBrandService, GroupBrandService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ILabelService, LabelService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IMerchantService, MerchantService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderItemService, OrderItemService>();
            services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();

            //catalog
            services.AddCodeFirstGrpcClient<ICategoryService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            }).AddInterceptor<GrpcExceptionInterceptor>();
            services.AddCodeFirstGrpcClient<IBrandService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            }).AddInterceptor<GrpcExceptionInterceptor>();
            services.AddCodeFirstGrpcClient<IGroupBrandService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            }).AddInterceptor<GrpcExceptionInterceptor>();

            services.AddCodeFirstGrpcClient<ILabelService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            }).AddInterceptor<GrpcExceptionInterceptor>();
            services.AddCodeFirstGrpcClient<ITagService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            }).AddInterceptor<GrpcExceptionInterceptor>();

            services.AddCodeFirstGrpcClient<IProductService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            }).AddInterceptor<GrpcExceptionInterceptor>();

            //factor
            services.AddCodeFirstGrpcClient<IMerchantService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            })
           .AddInterceptor<GrpcExceptionInterceptor>();

            //order

            services.AddCodeFirstGrpcClient<ICartService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            })
         .AddInterceptor<GrpcExceptionInterceptor>();
            services.AddCodeFirstGrpcClient<IOrderItemService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            })
         .AddInterceptor<GrpcExceptionInterceptor>();
            services.AddCodeFirstGrpcClient<IPurchaseOrderService>(o =>
            {
                o.Address = new Uri(configuration.GetConnectionString("AIO"));
            })
         .AddInterceptor<GrpcExceptionInterceptor>();
            return services;
        }
    }
}
