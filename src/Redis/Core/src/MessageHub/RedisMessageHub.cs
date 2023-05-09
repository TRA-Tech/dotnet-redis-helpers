using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace TraTech.Redis.MessageHub
{
    public class RedisMessageHub : IRedisMessageHub
    {
        private readonly RedisMessageHubOptions _redisMessageHubOptions;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ISubscriber _subscriber;

        public RedisMessageHub(IOptions<RedisMessageHubOptions> options, IServiceProvider serviceProvider)
        {
            _redisMessageHubOptions = options.Value;
            _serviceProvider = serviceProvider;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisMessageHubOptions.RedisConfigurationOptions);
            _subscriber = _connectionMultiplexer.GetSubscriber();
        }

        public async Task StartListening(string channelName)
        {
            if (string.IsNullOrEmpty(channelName)) throw new ArgumentException("it is null or empty", nameof(channelName));

            Type? handlerType = await _redisMessageHubOptions.RedisMessageHandlerProvider.GetHandlerAsync(channelName);
            if (handlerType == null) throw new NullReferenceException(nameof(handlerType));

            IRedisMessageHandler? service = _serviceProvider.GetService(handlerType) as IRedisMessageHandler;
            if (service == null) throw new NullReferenceException(nameof(service));

            ChannelMessageQueue channelQueue = await _subscriber.SubscribeAsync(channelName, CommandFlags.FireAndForget);
            channelQueue.OnMessage(async channelMessage =>
            {
                RedisValue message = channelMessage.Message;

                await service.HandleMessageAsync(message);
            });
        }

        public async Task StartListeningAll()
        {
            foreach (var channelName in _redisMessageHubOptions.RedisMessageHandlerProvider.ChannelNames())
            {
                await StartListening(channelName);
            }
        }
    }
}
