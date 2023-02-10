using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelCalculateSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> testData = CreateTestData(1000);

            //ProcessByParallel(testData, 8);
            //ProcessByParallel(testData, 4);
            //ProcessByParallel(testData, 2);
            //ProcessByParallel(testData, 1);

            ProcessByPlinq(testData, 12);
            ProcessByPlinq(testData, 8);
            ProcessByPlinq(testData, 4);
            ProcessByPlinq(testData, 2);
            ProcessByPlinq(testData, 1);

            //await ProcessByDataFlowAsync(testData, 8);
            //await ProcessByDataFlowAsync(testData, 4);
            //await ProcessByDataFlowAsync(testData, 2);
            //await ProcessByDataFlowAsync(testData, 1);

            ProcessBySignle(testData);

            Console.ReadLine();
        }

        /// <summary>
        /// 使用 For Each
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static List<string> ProcessBySignle(List<string> data)
        {
            DateTime start = DateTime.Now;

            List<string> results = new List<string>();
            
            foreach (var item in data)
            {
                if (ExecuteProcess(item)) 
                {
                    results.Add(item);
                }
            }

            DateTime end = DateTime.Now;

            Console.WriteLine($"ForEach: {results.Count} Records, { (end - start).TotalMilliseconds } ms");

            return results;
        }

        /// <summary>
        /// 使用 For Each
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static List<string> ProcessByLinqForEach(List<string> data)
        {
            DateTime start = DateTime.Now;

            List<string> results = new List<string>();

            data.ForEach((item) =>
            {
                if (ExecuteProcess(item))
                {
                    results.Add(item);
                }
            });

            DateTime end = DateTime.Now;

            Console.WriteLine($"ForEach: {results.Count} Records, { (end - start).TotalMilliseconds } ms");

            return results;
        }


        /// <summary>
        /// 使用 Parallel.ForEach
        /// </summary>
        /// <param name="data"></param>
        /// <param name="threads"></param>
        /// <returns></returns>
        static List<string> ProcessByParallel(List<string> data, int threads = 0)
        {
            DateTime start = DateTime.Now;

            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = (threads > 0) ? threads : Environment.ProcessorCount
            };

            var results = new ConcurrentStack<string>();
            Parallel.ForEach(data, options, (item, loopState) => 
            {
                if (ExecuteProcess(item)) {
                    results.Push(item);
                }
            });

            DateTime end = DateTime.Now;

            Console.WriteLine($"Parallel.ForEach ({threads} Threads): {results.Count} Records,  { (end - start).TotalMilliseconds } ms");

            return results.ToList();
        }

        /// <summary>
        /// 使用 Plinq
        /// </summary>
        /// <param name="data"></param>
        /// <param name="threads"></param>
        /// <returns></returns>
        static List<string> ProcessByPlinq(List<string> data, int threads)
        {
            DateTime start = DateTime.Now;

            int degreeOfParallelism = (threads > 0) ? threads : Environment.ProcessorCount;
            var results = new ConcurrentStack<string>();
            data.AsParallel()
                .WithDegreeOfParallelism(degreeOfParallelism)
                .ForAll(item => 
                {
                    if (ExecuteProcess(item)) 
                    {
                        results.Push(item);
                    }
                });

            DateTime end = DateTime.Now;

            Console.WriteLine($"Plinq ({threads} Threads): { (end - start).TotalMilliseconds } ms");

            return results.ToList();
        }

        /// <summary>
        /// 借用 DataFlow (封裝成擴充函式)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="threads"></param>
        /// <returns></returns>
        static async Task<List<string>> ProcessByDataFlowAsync(List<string> data, int threads)
        {
            DateTime start = DateTime.Now;

            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = (threads > 0) ? threads : Environment.ProcessorCount
            };

            var results = new ConcurrentStack<string>();
            await data.ParallelForEachAsync(options, async (item) => 
            {
                if (await ExecuteProcessAsync(item)) 
                {
                    results.Push(item);
                }
            });

            DateTime end = DateTime.Now;

            Console.WriteLine($"DataFlow ({threads} Threads): { (end - start).TotalMilliseconds } ms");

            return results.ToList();
        }

        static List<string> CreateTestData(int count)
        {
            List<string> testData = new List<string>();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    testData.Add(Guid.NewGuid().ToString());
                }
            }
            return testData;
        }

        static bool ExecuteProcess(string item)
        {
            Thread.Sleep(1);
            if (item.Contains("6") || item.Contains("5"))
            {
                return true;
            }           
            return false;
        }
        static Task<bool> ExecuteProcessAsync(string item)
        {
            Thread.Sleep(1);
            if (item.Contains("6") || item.Contains("5"))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
