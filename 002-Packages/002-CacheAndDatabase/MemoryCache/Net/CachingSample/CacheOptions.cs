using Microsoft.Extensions.Caching.Memory;

namespace CachingSample
{
    public class CacheOptions
    {
        /// <summary>
        /// 在指定的時間過期
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration;

        /// <summary>
        /// 多久後過期
        /// </summary>
        public TimeSpan? AbsoluteExpirationRelativeToNow;

        /// <summary>
        /// 多久未使用過期
        /// </summary>
        public TimeSpan? SlidingExpiration;

        /// <summary>
        /// 快取上限
        /// </summary>
        public long? Size;

        /// <summary>
        /// 優先權
        /// </summary>
        public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;
    }
}
