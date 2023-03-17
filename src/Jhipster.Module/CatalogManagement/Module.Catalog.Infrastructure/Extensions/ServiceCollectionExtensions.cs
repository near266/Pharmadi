
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Catalog.Application.Commands.BrandCm;
using Module.Catalog.Application.Persistences;
using Module.Catalog.Domain.Abstractions;
using Module.Catalog.Infrastructure.Persistence;
using Module.Catalog.Infrastructure.Persistence.Repositories;

namespace Module.Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<CatalogDbContext>(config)
                .AddScoped<ICatalogDbContext>(provider => provider.GetService<CatalogDbContext>());
            // Đăng kí mediatR
            services.AddMediatR(typeof(BrandAddCommand).Assembly);

            //// Đăng kí repository
            services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
            services.AddScoped(typeof(IGroupBrandRepository), typeof(GroupBrandRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICategoryProductRepository),typeof(CategoryProductRepository));
            services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
            services.AddScoped(typeof(ITagProductRepository), typeof(TagProductRepository));
            services.AddScoped(typeof(ILabelRepository), typeof(LabelRepository));
            services.AddScoped(typeof(ILabelProductRepository), typeof(LabelProductRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IWarehouseRepository), typeof(WarehouseRepository));
            services.AddScoped(typeof(IWarehouseProductRepository),typeof(WarehouseProductRepository));
            services.AddScoped(typeof(IPostContentRepository),typeof(PostContentRepository));
            return services;
        }
    }
}
