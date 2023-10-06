using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace TraTech.Redis.Core.MessageHub
{
    public static class RedisMessageHubServiceCollectionExtensions
    {
        public static RedisMessageHubBuilder AddRedisMessageHub(this IServiceCollection services, ConfigurationOptions configurationOptions)
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

            return new RedisMessageHubBuilder(services);
        }

        public static RedisMessageHubBuilder AddRedisMessageHub(this IServiceCollection services, string redisConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.Configure<RedisMessageHubOptions>(o =>
            {
                o.UseConfigurationOptions(ConfigurationOptions.Parse(redisConfiguration));
            });

            services.AddSingleton<RedisMessageHub>();

            return new RedisMessageHubBuilder(services);
        }
    }
}
