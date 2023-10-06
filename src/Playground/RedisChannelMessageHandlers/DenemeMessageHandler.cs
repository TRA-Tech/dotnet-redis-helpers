using TraTech.Redis.Abstractions.MessageHub;

namespace Playground.RedisChannelMessageHandlers
{
    public class DenemeMessageHandler : IRedisMessageHandler
    {
        public Task HandleMessageAsync(string? data)
        {
            Console.WriteLine(data);
            return Task.CompletedTask;
        }
    }
}
