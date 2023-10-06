using Newtonsoft.Json;
using StackExchange.Redis;

namespace TraTech.Redis.Core.Cache
{
    public class RedisCacheServiceOptions
    {
        private IConnectionMultiplexer? _connectionMultiplexer;

        public IConnectionMultiplexer ConnectionMultiplexer
        {
            get => _connectionMultiplexer ?? throw new ArgumentNullException(nameof(_connectionMultiplexer));
            set
            {
                _connectionMultiplexer = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public TimeSpan ExpireTime { get; set; }
        public JsonSerializerSettings? JsonSerializerSettings { get; }


        public RedisCacheServiceOptions()
        {
            ExpireTime = TimeSpan.FromDays(1);
            _connectionMultiplexer = null;
        }
    }
}
