using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SpringCloud.Connector.Redis
{
    public static class RedisCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services, IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddOptions();

            var redisOption = new RedisOption(config);

            Action<RedisCacheOptions> setupAction = options =>
            {
                options.Configuration = redisOption.ToString();
                options.InstanceName = redisOption.InstanceId;
            };

            services.Configure(setupAction);
            services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
            return services;
        }
    }
}