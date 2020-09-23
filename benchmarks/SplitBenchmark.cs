using split_string_with_span;
using System;
using BenchmarkDotNet.Attributes;

namespace benchmarks
{
    [MemoryDiagnoser]
    public class SplitBenchmark
    {
        private string _splittableString;

        private ReadOnlyMemory<char> _splittableMemory;

        [Params("test.aaa.PLEDASD", "...", "##fdsjrfjbkqe4293i4345n565gf.asd32434")]
        public string Str;

        [GlobalSetup]
        public void Setup()
        {
            _splittableMemory = Str.ToCharArray().AsMemory();
            _splittableString = Str;
        }

        [Benchmark]
        public void StringSplit()
        {
            foreach (var item in _splittableString.Split('.')) { _ = item; }
        }

        [Benchmark]
        public void SpanSplit()
        {
            foreach (var item in _splittableMemory.Span.Split('.')) { _ = item; }
        }
    }
}
