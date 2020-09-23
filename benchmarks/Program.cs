using BenchmarkDotNet.Running;

namespace benchmarks
{
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<SplitBenchmark>();
        }
    }
}
