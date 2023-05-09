using Newtonsoft.Json;
using StackExchange.Redis;

namespace TraTech.Redis.MessageHub
{
    public class RedisMessageHubOptions
    {
        public IRedisMessageHandlerProvider RedisMessageHandlerProvider { get; private set; }

        public ConfigurationOptions RedisConfigurationOptions { get; private set; }

        public JsonSerializerSettings? JsonSerializerSettings { get; }

        public RedisMessageHubOptions()
        {
            RedisMessageHandlerProvider = new RedisMessageHandlerProvider();
            RedisConfigurationOptions = new ConfigurationOptions();
        }

        public void UseRedisRequestHandlerProvider(IRedisMessageHandlerProvider redisMessageHandlerProvider)
        {
            RedisMessageHandlerProvider = redisMessageHandlerProvider ?? throw new ArgumentNullException(nameof(redisMessageHandlerProvider));
        }

        public void UseConfigurationOptions(ConfigurationOptions configurationOptions)
        {
            RedisConfigurationOptions = configurationOptions;
        }
    }
}
