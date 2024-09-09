namespace CachingSample
{
    public interface ICacheManager<TEntity>
    {
        Task<TEntity> GetAsync(string key, TEntity defaultEntity = default);

        Task<TEntity> GetAsync(string key, Func<Task<TEntity>>? cacheMissCallback);

        Task<bool> TryGetAsync(string key, out TEntity value);

        Task<TEntity> SetAsync(string key, TEntity value, CacheOptions options = null);

        Task RemoveAsync(string key);
    }
}
