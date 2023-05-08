using Microsoft.AspNetCore.Mvc;
using TraTech.Redis.Cache;

namespace ExampleWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;

        public WeatherForecastController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        [HttpGet("GetValueAsync")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await _redisCacheService.GetValueAsync(key);
            return Ok(value);
        }

        [HttpGet("SetValueAsync")]
        public async Task<IActionResult> Set(string key, string value)
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
    }
}