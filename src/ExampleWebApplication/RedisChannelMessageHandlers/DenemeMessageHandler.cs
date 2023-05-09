using TraTech.Redis.MessageHub;

namespace ExampleWebApplication.RedisChannelMessageHandlers
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
