using BenchmarkDotNet.Attributes;
using BenchmarkSample.Extensions;
using System.Collections.Concurrent;

namespace BenchmarkSample
{
    public class ParallelForEachCalculator
    {
        public List<string> TestData { get; set; }
        public int Threads { get; set; } = 4;

        public ParallelForEachCalculator() 
        {
            TestData = new List<string>();
            for(int i = 0; i < 100; i++)
            {
                TestData.Add(Guid.NewGuid().ToString());
            }
        }


        [Benchmark]
        public List<string> ProcessBySignle()
        {
            List<string> results = new List<string>();
            foreach (var item in TestData)
            {
                if (ExecuteProcess(item))
                {
                    results.Add(item);
                }
            }
            return results;
        }

        [Benchmark]
        public List<string> ProcessByParallel()
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = (Threads > 0) ? Threads : Environment.ProcessorCount
            };

            var results = new ConcurrentStack<string>();
            Parallel.ForEach(TestData, options, (item, loopState) =>
            {
                if (ExecuteProcess(item))
                {
                    results.Push(item);
                }
            });
            return results.ToList();
        }

        [Benchmark]
        public async Task<List<string>> ProcessByParallelAsync()
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = (Threads > 0) ? Threads : Environment.ProcessorCount
            };

            var results = new ConcurrentStack<string>();
            await Parallel.ForEachAsync(TestData, options, async (item, loopState) => {
                if (await ExecuteProcessAsync(item))
                {
                    results.Push(item);
                }
            });
            return results.ToList();
        }

        [Benchmark]
        public List<string> ProcessByPlinq()
        {
            int degreeOfParallelism = (Threads > 0) ? Threads : Environment.ProcessorCount;
            var results = new ConcurrentStack<string>();
            TestData.AsParallel()
                    .WithDegreeOfParallelism(degreeOfParallelism)
                    .ForAll(item =>
                    {
                        if (ExecuteProcess(item))
                        {
                            results.Push(item);
                        }
                    });
            return results.ToList();
        }

        [Benchmark]
        public async Task<List<string>> ProcessByDataFlowAsync()
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = (Threads > 0) ? Threads : Environment.ProcessorCount
            };

            var results = new ConcurrentStack<string>();
            await TestData.ParallelForEachAsync(options, async (item) =>
            {
                if (await ExecuteProcessAsync(item))
                {
                    results.Push(item);
                }
            });
            return results.ToList();
        }

        private bool ExecuteProcess(string item)
        {
            Thread.Sleep(1);
            if (item.Contains("6") || item.Contains("5"))
            {
                return true;
            }
            return false;
        }

        private Task<bool> ExecuteProcessAsync(string item)
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
