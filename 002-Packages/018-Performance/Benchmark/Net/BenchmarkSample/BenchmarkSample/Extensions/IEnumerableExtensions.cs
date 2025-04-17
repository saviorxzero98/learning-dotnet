using System.Threading.Tasks.Dataflow;

namespace BenchmarkSample.Extensions
{
    public static class IEnumerableExtensions
    {
        // can be removed in .NET 6 because it's built-in https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-6.0
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded
            };

            var block = new ActionBlock<T>(body, options);
            foreach (var item in source)
            {
                block.Post(item);
            }

            block.Complete();
            return block.Completion;
        }

        // can be removed in .NET 6 because it's built-in https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreachasync?view=net-6.0
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, ParallelOptions parallelOptions,
                                                   Func<T, Task> body)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = parallelOptions.MaxDegreeOfParallelism
            };

            if (parallelOptions.TaskScheduler != null)
            {
                options.TaskScheduler = parallelOptions.TaskScheduler;
            }

            var block = new ActionBlock<T>(body, options);

            foreach (var item in source)
            {
                block.Post(item);
            }

            block.Complete();
            return block.Completion;
        }
    }
}
