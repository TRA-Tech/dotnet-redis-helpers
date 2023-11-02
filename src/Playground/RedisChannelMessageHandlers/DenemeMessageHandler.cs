using Playground.Services;
using TraTech.Redis.Abstractions.MessageHub;

namespace Playground.RedisChannelMessageHandlers
{
    public class DenemeMessageHandler : IRedisMessageHandler
    {
        private Service2 _service2;

        public DenemeMessageHandler(Service2 service2)
        {
            _service2 = service2;
        }

        public Task HandleMessageAsync(string? data)
        {
            Console.WriteLine(data);
            return Task.CompletedTask;
        }
    }
}
