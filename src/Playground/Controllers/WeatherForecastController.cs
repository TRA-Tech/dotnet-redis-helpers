using Playground.Dtos;
using Microsoft.AspNetCore.Mvc;
using TraTech.Redis.Abstractions.Cache;
using TraTech.Redis.Abstractions.MessageHub;
using TraTech.Redis.Core.MessageHub;

namespace Playground.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly IRedisMessageHub? _redisMessageHub;

        public WeatherForecastController(IRedisCacheService redisCacheService, IServiceProvider serviceProvider)
        {
            _redisCacheService = redisCacheService;
            _redisMessageHub = (serviceProvider.GetService(typeof(RedisMessageHub)) as RedisMessageHub);
        }

        [HttpGet("GetValueAsync")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await _redisCacheService.GetValueAsync<TestSetDto>(key);
            return Ok(value);
        }

        [HttpPost("SetValueAsync")]
        public async Task<IActionResult> Set(string key, TestSetDto value)
        {
            await _redisCacheService.SetValueAsync(key, value);
            return Ok();
        }

        [HttpGet("GetOrAddAsync")]
        public async Task<IActionResult> GetOrAddAsync(string key, string value)
        {
            var result = await _redisCacheService.GetOrAddAsync(key, () => Task.FromResult(value));
            return Ok(result);
        }

        [HttpGet("GetOrAdd")]
        public IActionResult GetOrAdd(string key, string value)
        {
            var result = _redisCacheService.GetOrAdd(key, () => value);
            return Ok(result);
        }

        [HttpGet("ClearAsync")]
        public async Task<IActionResult> ClearAsync(string key)
        {
            await _redisCacheService.ClearAsync(key);
            return Ok();
        }

        [HttpGet("ClearAll")]
        public IActionResult ClearAll()
        {
            _redisCacheService.ClearAll();
            return Ok();
        }

        [HttpGet("PublishAsync")]
        public async Task<IActionResult> PublishAsync(string key, string channelName)
        {
            var cacheData = await _redisCacheService.GetValueAsync<TestSetDto>(key);

            await _redisMessageHub.PublishAsync(channelName, cacheData);

            return Ok();
        }
    }
}