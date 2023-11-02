using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace TraTech.Redis.Core.MessageHub
{
    public static class RedisMessageHubServiceCollectionExtensions
    {
        public static RedisMessageHubBuilder AddRedisMessageHub(this IServiceCollection services, ConfigurationOptions configurationOptions, ServiceLifetime serviceLifetimeOfHandlers = ServiceLifetime.Transient)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.Configure<RedisMessageHubOptions>(o =>
            {
                o.UseConfigurationOptions(configurationOptions);
            });

            services.AddSingleton<RedisMessageHub>();

            return new RedisMessageHubBuilder(services, serviceLifetimeOfHandlers);
        }

        public static RedisMessageHubBuilder AddRedisMessageHub(this IServiceCollection services, string redisConfiguration, ServiceLifetime serviceLifetimeOfHandlers = ServiceLifetime.Transient)
            => AddRedisMessageHub(services, ConfigurationOptions.Parse(redisConfiguration), serviceLifetimeOfHandlers);
    }
}
