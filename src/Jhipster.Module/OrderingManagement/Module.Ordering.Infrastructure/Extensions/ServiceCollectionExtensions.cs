
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Factor.Infrastructure.Persistence.Repositories;
using Module.Ordering.Application.Commands.CartCm;
using Module.Ordering.Application.Persistences;
using Module.Ordering.Domain.Abstractions;
using Module.Ordering.Infrastructure.Persistence.Repositories;
using Module.Ordering.Infrastructure.Persistences;


namespace Module.Ordering.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<OrderingDbContext>(config)
                .AddScoped<IOrderingDbContext>(provider => provider.GetService<OrderingDbContext>());
            // Đăng kí mediatR
            services.AddMediatR(typeof(CartAddCommand).Assembly);

            //// Đăng kí repository
            services.AddScoped(typeof(ICartRepostitory), typeof(CartRepository));
            services.AddScoped(typeof(IOrderItemRepostitory), typeof(OrderItemRepository));
            services.AddScoped(typeof(IPurchaseOrderRepostitory), typeof(PurchaseOrderRepository));
             services.AddScoped(typeof(IOrderStatusRepostitory),typeof(OrderStatusRepository));
            services.AddScoped(typeof(IProductSaleRepostitory), typeof(ProductSaleRepository));
            return services;
        }
           
    }
}
