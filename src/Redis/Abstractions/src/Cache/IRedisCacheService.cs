namespace TraTech.Redis.Cache
{
    public interface IRedisCacheService
    {
        public Task<string?> GetValueAsync(string key);
        public Task<bool> SetValueAsync(string key, string value);
        public Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class;
        public T GetOrAdd<T>(string key, Func<T> action) where T : class;
        public Task ClearAsync(string key);
        public void ClearAll();
    }
}
