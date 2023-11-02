using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;
using TraTech.Redis.Abstractions.MessageHub;

namespace TraTech.Redis.Core.MessageHub
{
    public class RedisMessageHubBuilder
    {
        public virtual ServiceLifetime ServiceLifetime { get; }
        public virtual IServiceCollection Services { get; }

        public RedisMessageHubBuilder(IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            Services = services;
            ServiceLifetime = serviceLifetime;
        }

        public RedisMessageHubBuilder AddChannelMessageHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string channelName, ServiceLifetime? serviceLifetime = null)
            where THandler : class, IRedisMessageHandler
        {
            Services.Configure<RedisMessageHubOptions>(o =>
            {
                o.RedisMessageHandlerProvider.TryAddHandler<THandler>(channelName);
            });

            Services.TryAdd(new ServiceDescriptor(typeof(THandler), typeof(THandler), serviceLifetime ?? ServiceLifetime));

            return this;
        }
    }
}
