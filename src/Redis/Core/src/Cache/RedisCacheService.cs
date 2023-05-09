using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace TraTech.Redis.Cache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _cache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IOptions<RedisCacheServiceOptions> _options;

        public RedisCacheService(IOptions<RedisCacheServiceOptions> options)
        {
            _options = options;
            _connectionMultiplexer = options.Value.ConnectionMultiplexer;
            _cache = options.Value.ConnectionMultiplexer.GetDatabase();
        }

        private byte[] EncodeData<T>(T data) where T : class
        {
            return Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(data, _options.Value.JsonSerializerSettings)
            );
        }

        public async Task<T?> GetValueAsync<T>(string key) where T : class
        {
            RedisValue result = await _cache.StringGetAsync(key);

            if (result.IsNull)
                return null;

            return JsonConvert.DeserializeObject<T>(result, _options.Value.JsonSerializerSettings);
        }

        public async Task<bool> SetValueAsync<T>(string key, T value)
        {
            string data = JsonConvert.SerializeObject(value, _options.Value.JsonSerializerSettings);
            return await _cache.StringSetAsync(key, data, _options.Value.ExpireTime);
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            RedisValue result = await _cache.StringGetAsync(key);
            if (result.IsNull)
            {
                result = EncodeData(await action());
                await SetValueAsync(key, result);
            }

            return JsonConvert.DeserializeObject<T>(result, _options.Value.JsonSerializerSettings);
        }

        public T GetOrAdd<T>(string key, Func<T> action) where T : class
        {
            var result = _cache.StringGet(key);
            if (result.IsNull)
            {
                result = EncodeData(action());
                _cache.StringSet(key, result, _options.Value.ExpireTime);
            }
            return JsonConvert.DeserializeObject<T>(result, _options.Value.JsonSerializerSettings);
        }

        public async Task ClearAsync(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var endpoints = _connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
    }
}
