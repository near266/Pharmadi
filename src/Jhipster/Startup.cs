using System;
using Jhipster.Infrastructure.Data;
using Jhipster.Configuration;
using Jhipster.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Jhipster.Infrastructure.Shared;
using Serilog;
using ProtoBuf.Grpc.Server;
using Module.Catalog;
using MediatR;
using System.Reflection;
using BFF.Web;
using Module.Factor;
using Module.Factor.Services;
using Module.Permission;
using Module.Ordering;

[assembly: ApiController]

namespace Jhipster
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAppSettingsModule(Configuration);

            
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            AddDatabase(services);

            AddModule(services);

            AddBffGateway(services);

            //AddSerilog(Configuration);

            services
                .AddSecurityModule()
                .AddProblemDetailsModule(Environment)
                .AddAutoMapperModule()
                .AddSwaggerModule()
                .AddWebModule()
                .AddRepositoryModule()
                .AddServiceModule();

            services
                .AddMailModule(Configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider,
            ApplicationDatabaseContext context, IOptions<SecuritySettings> securitySettingsOptions)
        {
            var securitySettings = securitySettingsOptions.Value;
            app.UseSerilogRequestLogging();
            app
                .UseApplicationSecurity(securitySettings)
                .UseApplicationProblemDetails()
                .UseApplicationSwagger()
                .UseApplicationWeb(env)
                .UseApplicationDatabase(serviceProvider, env)
                .UseApplicationIdentity(serviceProvider);

            AddAppModule(app);
        }

        protected virtual void AddDatabase(IServiceCollection services)
        {
            services.AddDatabaseModule(Configuration);
        }

        protected virtual void AddModule(IServiceCollection services)
        {
            // General Infras
            services.AddSharedInfrastructure(Configuration);

            // Module
            services.AddCatalogModule(Configuration);
            services.AddFactorModule(Configuration);
            services.AddPermissionModule(Configuration);
            services.AddOrderingModule(Configuration);
            //services.AddBasketModule(Configuration);
            //// Redis
            //services.AddRedisModule(Configuration);
        }

        protected virtual void AddAppModule(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchantService>();

                //endpoints.MapGrpcService<BasketService>();
                //endpoints.MapGrpcService<CatalogService>();

                //endpoints.MapGrpcService<OrderingService>();
                //endpoints.MapGrpcService<OrderItemService>();
                ////endpoints.MapGrpcReflectionService();
                endpoints.MapCodeFirstGrpcReflectionService();
            });
        }

        protected virtual void AddBffGateway(IServiceCollection services)
        {
            //services.AddBFFWebModule(Configuration);
            //services.AddCodeFirstGrpcReflection();
        }
    }
}
