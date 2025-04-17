using BenchmarkDotNet.Running;

namespace BenchmarkSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ParallelForEachCalculator>();
        }
    }
}
