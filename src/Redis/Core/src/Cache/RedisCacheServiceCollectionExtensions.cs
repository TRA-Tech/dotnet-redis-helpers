using Microsoft.Extensions.DependencyInjection;

namespace TraTech.Redis.Cache
{
    public static class RedisCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCacheService(this IServiceCollection services, Action<RedisCacheServiceOptions> configureOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.AddOptions();
            services.Configure(configureOptions);

            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            return services;
        }
    }
}
