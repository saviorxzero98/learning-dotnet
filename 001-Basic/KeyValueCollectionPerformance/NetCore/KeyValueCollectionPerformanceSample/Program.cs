using System;
using System.Collections.Generic;

namespace KeyValueCollectionPerformanceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            

            int count = 1000000;
            List<int> data = GetTestData(count);

            // Serial
            //var serialSample = new CollectionSerialAccessSample();
            //serialSample.TestDictionaryUnlock(data);
            //serialSample.TestConcurrentDictionary(data);
            //serialSample.TestMemoryCache(data);

            // Parallel
            var parallelSample = new CollectionParallelAccessSample();
            //parallelSample.TestDictionaryUnlock(data);
            parallelSample.TestDictionaryLock(data);
            parallelSample.TestConcurrentDictionary(data);
            parallelSample.TestMemoryCache(data);
        }

        static List<int> GetTestData(int count)
        {
            List<int> data = new List<int>();

            for (int i = 0; i < count; i++)
            {
                data.Add(i);
            }

            return data;
        }
    }
}
