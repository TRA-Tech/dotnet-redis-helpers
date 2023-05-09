using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace TraTech.Redis.MessageHub
{
    public class RedisMessageHubBuilder
    {
        public RedisMessageHubBuilder(IServiceCollection services) => Services = services;

        public virtual IServiceCollection Services { get; }

        public RedisMessageHubBuilder AddChannelMessageHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string channelName)
            where THandler : class, IRedisMessageHandler
        {
            Services.Configure<RedisMessageHubOptions>(o =>
            {
                o.RedisMessageHandlerProvider.TryAddHandler<THandler>(channelName);
            });

            Services.TryAddTransient<THandler>();

            return this;
        }
    }
}
