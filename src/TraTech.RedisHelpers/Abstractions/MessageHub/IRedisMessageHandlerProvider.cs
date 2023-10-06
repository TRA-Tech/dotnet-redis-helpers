using System.Diagnostics.CodeAnalysis;

namespace TraTech.Redis.Abstractions.MessageHub
{
    public interface IRedisMessageHandlerProvider
    {
        public Task<Type?> GetHandlerAsync(string channelName);

        public void AddHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string channelName) where THandler : class, IRedisMessageHandler;

        public bool TryAddHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string channelName) where THandler : class, IRedisMessageHandler;

        public IEnumerable<string> ChannelNames();
    }
}
