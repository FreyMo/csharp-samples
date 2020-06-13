using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace system_linq_async
{
    class Program
    {
        static async Task Main()
        {
            var timeA = await Measure(A);
            var timeB = await Measure(B);

            Console.WriteLine($"A took {timeA}");
            Console.WriteLine($"B took {timeB}");

            Console.WriteLine("DONE");
        }

        static async Task A()
        {
            // Enumerates one by one
            await foreach (var number in ProduceNumbersAsync())
            {
                Console.WriteLine($"{number}");
            }

            Console.WriteLine("A DONE");
        }

        static async Task B()
        {
            // Waits until all are enumerated
            foreach (var number in await ProduceNumbersAsync().ToListAsync())
            {
                Console.WriteLine($"{number}");
            }

            Console.WriteLine("B DONE");
        }


        static async Task<TimeSpan> Measure(Func<Task> func)
        {
            var stopWatch = Stopwatch.StartNew();

            await func();

            stopWatch.Stop();

            return stopWatch.Elapsed;
        }

        static async IAsyncEnumerable<int> ProduceNumbersAsync()
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(10);
                yield return i;
            }
        }

        // Pros:
        //      More concise, closer to synchronous
        //      yield as a keyword, not as a type

        // Cons:
        //      No ParallelAsync
    }
}
