namespace TraTech.Redis.MessageHub
{
    public interface IRedisMessageHandler
    {
        public Task HandleMessageAsync(string? data);
    }
}
