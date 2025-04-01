using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace DistributedCachingSample.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T?> GetDataOrDefaultAsync<T>(this IDistributedCache cache, string key)
        {
            var dataBytes = await cache.GetAsync(key);

            var dataJson = await cache.GetStringAsync(key);

            try
            {
                if (!string.IsNullOrWhiteSpace(dataJson))
                {
                    return JsonConvert.DeserializeObject<T>(dataJson);
                }
            }
            catch
            {

            }
            return default(T);
        }

        public static async Task SetDataAsync<T>(this IDistributedCache cache, string key, T data)
        {
            if (data == null)
            {
                return;
            }

            var dataJson = JsonConvert.SerializeObject(data);

            await cache.SetStringAsync(key, dataJson);
        }
    }
}
