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
            var isDisposed = _service2.Service1.Disposed;
            var name = _service2.GetName();
            Console.Out.WriteLine(isDisposed);
            Console.Out.WriteLine(name);
            return Task.CompletedTask;
        }
    }
}
