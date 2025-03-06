using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace DistributedCachingSample.Services
{
    public class MemoryCacheManager<TEntity> : IDistributedCacheManager<TEntity>
    {
        private readonly IDistributedCache _cache;
        private readonly CacheOptions _options;

        public MemoryCacheManager(IDistributedCache cache)
        {
            _cache = cache;
            _options = new CacheOptions();
        }
        public MemoryCacheManager(IDistributedCache cache, CacheOptions option)
        {
            _cache = cache;
            _options = option;
        }

        public async Task<TEntity?> GetAsync(string key,
                                            TEntity? defaultEntity = default)
        {
            var result = await TryGetCacheDataAsync(key, defaultEntity);
            return (result.IsHit && result.Data != null) ? result.Data : defaultEntity;
        }

        public async Task<TEntity?> GetAsync(string key,
                                            Func<Task<TEntity?>>? cacheMissCallback)
        {
            var result = await TryGetCacheDataAsync(key);
            if (result.IsHit)
            {
                return result.Data;
            }

            if (cacheMissCallback == null)
            {
                return default(TEntity);
            }

            var newValue = await cacheMissCallback.Invoke();
            return newValue;
        }

        public async Task<TEntity?> GetOrAddAsync(string key, Func<Task<TEntity?>>? cacheMissCallback)
        {
            var result = await TryGetCacheDataAsync(key);
            if (result.IsHit)
            {
                return result.Data;
            }

            if (cacheMissCallback == null)
            {
                return default(TEntity);
            }

            var newValue = await cacheMissCallback.Invoke();

            if (newValue != null)
            {
                await SetCacheDataAsync(key, newValue);
            }
            return newValue;
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(IEnumerable<string> keys)
        {
            var dataList = new List<TEntity>();

            foreach (var key in keys)
            {
                var result = await TryGetCacheDataAsync(key);
                
                if (result.IsHit && result.Data != null)
                {
                    dataList.Add(result.Data);
                }
            }
            return dataList;
        }


        public async Task<TEntity?> SetAsync(string key, TEntity value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            await SetCacheDataAsync(key, value);
            return value;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
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


        #region Common

        protected virtual async Task<CacheDataResult<TEntity>> TryGetCacheDataAsync(string key, TEntity? defaultValue = default)
        {
            var dataJson = await _cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(dataJson))
            {
                var data = JsonConvert.DeserializeObject<TEntity>(dataJson);
                if (data != null)
                {
                    return new CacheDataResult<TEntity>(key, data, isHit: true);
                }
            }
            return new CacheDataResult<TEntity>(key);
        }

        protected virtual async Task SetCacheDataAsync(string key, TEntity data)
        {
            if (data == null)
            {
                await _cache.RemoveAsync(key);
                return;
            }

            var dataJson = JsonConvert.SerializeObject(data);
            _cache.SetString(key, dataJson);
        }

        #endregion
    }
}
