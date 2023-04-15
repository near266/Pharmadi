using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using AspNetCoreRateLimit.Redis;
using AspNetCoreRateLimit;
using Module.Redis.Library.Share;
using Newtonsoft.Json;
using System.Text;
using Module.Redis.Configurations;

namespace Module.Redis
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddRedisModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            ConfigurationOptions? redisOptions;
            services.AddSingleton<RedisConfig>(serviceProvider =>
            {

                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                // var json = Encoding.UTF8.GetString(configuration.GetValue<byte[]>(Constant.REDIS));
                //  var json = configuration.GetValue<string>(Constant.REDIS);
                var Configuration = configuration.GetValue<string>("Redis:Configuration");
                var InstanceName = configuration.GetValue<string>("Redis:InstanceName");
                var RedisEnabled = configuration.GetValue<string>("Redis:RedisEnabled");
                var RedisKey = configuration.GetValue<string>("Redis:RedisKey");
                var x = new RedisConfig()
                {
                    Configuration = Configuration,
                    InstanceName = InstanceName,
                    RedisEnabled = bool.Parse(RedisEnabled),
                    RedisKey = RedisKey,



                };

                return x;
                // return JsonConvert.DeserializeObject<RedisConfig>(json) ?? throw new ArgumentNullException(nameof(RedisConfig));
            });
            if (bool.Parse(configuration["Redis:RedisEnabled"]))
            {
                services.AddStackExchangeRedisCache(options =>
                {

                    options.Configuration = configuration["Redis:Configuration"];
                    options.InstanceName = configuration["Redis:InstanceName"];

                });
                services.Configure<IpRateLimitOptions>(configuration.GetSection(Constant.RATELIMIT));
                redisOptions = ConfigurationOptions.Parse(configuration["Redis:Configuration"]);
                services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
                services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
                services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
                services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
                services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(redisOptions));
                services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
                services.AddDistributedRateLimiting<RedisProcessingStrategy>();
                services.AddRedisRateLimiting();


            }



            return services;
        }

    }

}
