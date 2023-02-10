using Microsoft.Extensions.Caching.Memory;
using System;

namespace MemoryCacheSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            var obj = new { 
                Id = 1
            };
            string key = "myKey";
            // 寫入快取資料
            cache.Set(key, obj);

            // 讀出快取資料
            var obj2 = cache.Get(key);

            Console.WriteLine("Hello World!");
        }
    }
}
