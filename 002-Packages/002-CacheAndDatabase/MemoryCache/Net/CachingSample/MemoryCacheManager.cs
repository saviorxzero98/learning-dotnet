using Microsoft.Extensions.Caching.Memory;

namespace CachingSample
{
    public class MemoryCacheManager<TEntity> : ICacheManager<TEntity>
    {
        private readonly IMemoryCache _cache;
        private readonly CacheOptions _options;

        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public MemoryCacheManager(IMemoryCache cache, CacheOptions option)
        {
            _cache = cache;
            _options = option;
        }

        public async Task<TEntity> GetAsync(string key,
                                            TEntity defaultEntity = default)
        {
            if (_cache.TryGetValue(key, out TEntity cacheValue))
            {
                return cacheValue;
            }

            return defaultEntity;
        }
        public async Task<TEntity> GetAsync(string key,
                                            Func<Task<TEntity>>? cacheMissCallback)
        {
            if (_cache.TryGetValue(key, out TEntity cacheValue))
            {
                return cacheValue;
            }

            if (cacheMissCallback == null)
            {
                return default(TEntity);
            }

            var newValue = await cacheMissCallback.Invoke();
            return newValue;
        }

        public Task<bool> TryGetAsync(string key, out TEntity value)
        {
            var result = _cache.TryGetValue(key, out value);
            return Task.FromResult(result);
        }

        public Task<TEntity> SetAsync(string key, TEntity value, CacheOptions options = null)
        {
            TEntity cacheValue;
            if (options != null)
            {
                cacheValue = _cache.Set(key, value, ToEntryOptions(options));
            }
            else
            {
                cacheValue = _cache.Set(key, value, ToEntryOptions(_options));
            }
            return Task.FromResult(cacheValue);
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        protected MemoryCacheEntryOptions? ToEntryOptions(CacheOptions options)
        {
            if (options == null)
            {
                return default(MemoryCacheEntryOptions);
            }

            var entryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow,
                SlidingExpiration = options.SlidingExpiration,
                Size = options.Size,
                Priority = options.Priority
            };
            return entryOptions;
        }
    }
}
