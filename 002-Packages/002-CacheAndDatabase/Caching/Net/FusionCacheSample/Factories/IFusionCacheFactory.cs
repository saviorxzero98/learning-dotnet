using ZiggyCreatures.Caching.Fusion;

namespace FusionCacheSample.Factories
{
    public interface IFusionCacheFactory
    {
        IFusionCache GetFusionCache();
    }
}
