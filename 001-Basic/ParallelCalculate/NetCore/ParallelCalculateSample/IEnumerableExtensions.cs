using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ParallelCalculateSample
{
    public static class IEnumerableExtensions
    {
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
