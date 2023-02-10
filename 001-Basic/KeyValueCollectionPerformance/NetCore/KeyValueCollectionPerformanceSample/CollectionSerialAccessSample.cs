using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace KeyValueCollectionPerformanceSample
{
    public class CollectionSerialAccessSample
    {
        public void TestDictionaryUnlock(List<int> data)
        {
            var dict = new Dictionary<string, int>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach(int item in data)
            {
                dict[$"Key${item}"] = item;
            }

            Console.WriteLine($"Dictionary (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            foreach (int item in data)
            {
                var value = dict[$"Key${item}"];
            }

            Console.WriteLine($"Dictionary (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }

        public void TestConcurrentDictionary(List<int> data)
        {
            var dict = new ConcurrentDictionary<string, int>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (int item in data)
            {
                dict.TryAdd($"Key${item}", item);
            }

            Console.WriteLine($"ConcurrentDictionary (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            foreach (int item in data)
            {
                dict.TryGetValue($"Key${item}", out int value);
            }

            Console.WriteLine($"ConcurrentDictionary (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }

        public void TestMemoryCache(List<int> data)
        {
            var dict = new MemoryCache(new MemoryCacheOptions());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (int item in data)
            {
                dict.Set($"Key${item}", item);
            }

            Console.WriteLine($"Memory Cache (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            foreach (int item in data)
            {
                dict.TryGetValue($"Key${item}", out int value);
            }

            Console.WriteLine($"Memory Cache (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }
    }
}
