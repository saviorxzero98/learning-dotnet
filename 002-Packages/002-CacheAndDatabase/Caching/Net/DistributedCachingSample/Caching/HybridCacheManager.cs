using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;

namespace DistributedCachingSample.Caching
{
    public class HybridCacheManager : ICacheManager
    {
        private readonly HybridCache _cache;
        private readonly HybridCacheEntryOptions _options;

        public HybridCacheManager(
            HybridCache cache,
            IOptions<HybridCacheEntryOptions> options)
        {
            _cache = cache;
            _options = options.Value;
        }

        public virtual ValueTask<T> GetOrCreateAsync<T>(
            string key,
            Func<CancellationToken, ValueTask<T>> factory,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default)
        {
            return _cache.GetOrCreateAsync(key, factory, _options, tags, cancellationToken);
        }

        public virtual ValueTask SetAsync<T>(
            string key,
            T value,
            IEnumerable<string>? tags = null,
            CancellationToken cancellationToken = default)
        {
            return _cache.SetAsync(key, value, _options, tags, cancellationToken);
        }

        public virtual ValueTask RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return _cache.RemoveAsync(key, cancellationToken);
        }

        public virtual ValueTask RemoveAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            return _cache.RemoveAsync(keys, cancellationToken);
        }

        public virtual ValueTask RemoveByTagAsync(IEnumerable<string> tags, CancellationToken cancellationToken = default)
        {
            return _cache.RemoveByTagAsync(tags, cancellationToken);
        }

        public virtual ValueTask RemoveByTagAsync(string tag, CancellationToken cancellationToken = default)
        {
            return _cache.RemoveByTagAsync(tag, cancellationToken);
        }
    }
}
