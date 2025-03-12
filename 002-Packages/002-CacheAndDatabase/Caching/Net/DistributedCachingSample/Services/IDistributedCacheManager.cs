namespace DistributedCachingSample.Services
{
    public interface IDistributedCacheManager<TEntity>
    {
        Task<TEntity?> GetAsync(string key, TEntity? defaultEntity = default);

        Task<TEntity?> GetAsync(string key, Func<Task<TEntity?>>? cacheMissCallback);

        Task<TEntity?> GetOrAddAsync(string key, Func<Task<TEntity?>>? cacheMissCallback);

        Task<IEnumerable<TEntity>> GetManyAsync(IEnumerable<string> keys);

        Task<TEntity?> SetAsync(string key, TEntity value);

        Task RemoveAsync(string key);
    }
}
