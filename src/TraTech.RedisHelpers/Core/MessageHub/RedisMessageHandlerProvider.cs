using System.Diagnostics.CodeAnalysis;
using TraTech.Redis.Abstractions.MessageHub;

namespace TraTech.Redis.Core.MessageHub
{
    public class RedisMessageHandlerProvider : IRedisMessageHandlerProvider
    {
        private readonly object _lock = new();
        private readonly Dictionary<string, Type> _handlerTypeMap = new(StringComparer.Ordinal);

        public bool TryAddHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string channelName)
            where THandler : class, IRedisMessageHandler
        {
            if (_handlerTypeMap.ContainsKey(channelName))
            {
                return false;
            }
            if (!typeof(IRedisMessageHandler).IsAssignableFrom(typeof(THandler)))
            {
                throw new InvalidOperationException($"{typeof(THandler)} is not assignable from IRedisRequestHandler");
            }

            lock (_lock)
            {
                _handlerTypeMap.Add(channelName, typeof(THandler));
                return true;
            }
        }

        public void AddHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string channelName)
            where THandler : class, IRedisMessageHandler
        {
            if (_handlerTypeMap.ContainsKey(channelName))
            {
                throw new InvalidOperationException("RedisRequestHandler already exists for " + channelName);
            }
            lock (_lock)
            {
                if (!TryAddHandler<THandler>(channelName))
                {
                    throw new InvalidOperationException("RedisRequestHandler already exists for " + channelName);
                }
            }
        }

        public Task<Type?> GetHandlerAsync(string messageType)
        {
            return Task.FromResult(_handlerTypeMap.TryGetValue(messageType, out var handlerType) ? handlerType : null);
        }

        public IEnumerable<string> ChannelNames()
        {
            return _handlerTypeMap.Keys;
        }
    }
}
