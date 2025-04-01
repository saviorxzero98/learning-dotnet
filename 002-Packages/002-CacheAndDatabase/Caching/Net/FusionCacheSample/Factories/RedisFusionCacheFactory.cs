using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Backplane;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace FusionCacheSample.Factories
{
    public class RedisFusionCacheFactory : IFusionCacheFactory
    {
        protected readonly IFusionCache _fusionCache;
        protected readonly IFusionCacheBackplane _backplane;
        protected readonly IDistributedCache _distributedCache;

        public RedisFusionCacheFactory(
            IFusionCache fusionCache,
            IFusionCacheBackplane backplane,
            IDistributedCache distributedCache)
        {
            _fusionCache = fusionCache;
            _backplane = backplane;
            _distributedCache = distributedCache;
        }

        public IFusionCache GetFusionCache()
        {
            var serializer = new FusionCacheNewtonsoftJsonSerializer(new JsonSerializerSettings()
            {

            });

            return _fusionCache.SetupSerializer(serializer)
                               .SetupDistributedCache(_distributedCache)
                               .SetupBackplane(_backplane);
        }
    }
}
