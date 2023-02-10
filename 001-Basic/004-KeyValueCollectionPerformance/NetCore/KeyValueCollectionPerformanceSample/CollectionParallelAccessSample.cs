using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KeyValueCollectionPerformanceSample
{
    public class CollectionParallelAccessSample
    {
        public void TestDictionaryUnlock(List<int> data, int threads = 4)
        {
            var dict = new Dictionary<string, int>();
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = threads
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(data, options, (item) =>
            {
                string key = $"Key${item}";
                if (dict.ContainsKey(key))
                {
                    dict[key] = item;
                }
                else
                {
                    dict.Add(key, item);
                }
            });

            Console.WriteLine($"Dictionary (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            Parallel.ForEach(data, options, (item) =>
            {
                string key = $"Key${item}";
                if (dict.ContainsKey(key))
                {
                    var value = dict[key];
                }
            });

            Console.WriteLine($"Dictionary (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }

        public void TestDictionaryLock(List<int> data, int threads = 4)
        {
            var dict = new Dictionary<string, int>();
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = threads
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(data, options, (item) =>
            {
                lock(dict)
                {
                    string key = $"Key${item}";
                    if (dict.ContainsKey(key))
                    {
                        dict[key] = item;
                    }
                    else
                    {
                        dict.Add(key, item);
                    }
                }
            });

            Console.WriteLine($"Dictionary (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            Parallel.ForEach(data, options, (item) =>
            {
                lock(dict)
                {
                    string key = $"Key${item}";
                    if (dict.ContainsKey(key))
                    {
                        var value = dict[key];
                    }
                }
            });

            Console.WriteLine($"Dictionary (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }

        public void TestConcurrentDictionary(List<int> data, int threads = 4)
        {
            var dict = new ConcurrentDictionary<string, int>();
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = threads
            };


            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(data, options, (item) =>
            {
                dict.TryAdd($"Key${item}", item);
            });

            Console.WriteLine($"ConcurrentDictionary (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            Parallel.ForEach(data, options, (item) =>
            {
                dict.TryGetValue($"Key${item}", out int value);
            });

            Console.WriteLine($"ConcurrentDictionary (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }

        public void TestMemoryCache(List<int> data, int threads = 4)
        {
            var dict = new MemoryCache(new MemoryCacheOptions());
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = threads
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(data, options, (item) =>
            {
                dict.Set($"Key${item}", item);
            });

            Console.WriteLine($"Memory Cache (Write) : {sw.Elapsed.TotalMilliseconds}");

            sw.Restart();

            Parallel.ForEach(data, options, (item) =>
            {
                dict.TryGetValue($"Key${item}", out int value);
            });

            Console.WriteLine($"Memory Cache (Read) : {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();
        }
    }
}
