namespace TraTech.Redis.MessageHub
{
    public interface IRedisMessageHub
    {
        public Task StartListening(string channelName);

        public Task StartListeningAll();
    }
}
