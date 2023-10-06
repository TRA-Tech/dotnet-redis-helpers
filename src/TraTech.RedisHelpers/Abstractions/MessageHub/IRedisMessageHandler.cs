namespace TraTech.Redis.Abstractions.MessageHub
{
    public interface IRedisMessageHandler
    {
        public Task HandleMessageAsync(string? data);
    }
}
