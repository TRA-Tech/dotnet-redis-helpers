namespace TraTech.Redis.Cache
{
    public interface IRedisCacheService
    {
        public Task<T?> GetValueAsync<T>(string key) where T : class;
        public Task<bool> SetValueAsync<T>(string key, T value);
        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
        public T GetOrAdd<T>(string key, Func<T> action) where T : class;
        public Task ClearAsync(string key);
        public void ClearAll();
    }
}
