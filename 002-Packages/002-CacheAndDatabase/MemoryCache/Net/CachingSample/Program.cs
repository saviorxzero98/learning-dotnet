using Microsoft.Extensions.Caching.Memory;

namespace CachingSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var manager = new MemoryCacheManager<string>(cache, new CacheOptions());
            var obj = "myData";
            string key = "myKey";
            
            // 寫入快取資料
            await manager.SetAsync(key, obj);

            // 讀出快取資料
            var obj2 = await manager.GetAsync(key);

            Console.WriteLine(obj2);
        }
    }
}
