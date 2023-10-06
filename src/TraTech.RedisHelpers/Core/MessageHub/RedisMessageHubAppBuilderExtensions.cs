using Microsoft.AspNetCore.Builder;

namespace TraTech.Redis.Core.MessageHub
{
    public static class RedisMessageHubAppBuilderExtensions
    {
        public static async Task<IApplicationBuilder> UseRedisMessageHub(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            RedisMessageHub? redisMessageHub = app.ApplicationServices.GetService(typeof(RedisMessageHub)) as RedisMessageHub;
            if (redisMessageHub == null) throw new NullReferenceException(nameof(redisMessageHub));
            await redisMessageHub.StartListeningAll();

            return app;
        }
    }
}
