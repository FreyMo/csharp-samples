using System;
using Dasync.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace async_enumerator
{
    class Program
    {
        static async Task Main()
        {
            var timeA = await Measure(A);
            var timeB = await Measure(B);
            var timeC = await Measure(C);

            Console.WriteLine($"A took {timeA}");
            Console.WriteLine($"B took {timeB}");
            Console.WriteLine($"C took {timeC}");

            Console.WriteLine("DONE");
        }

        static async Task A()
        {
            // Enumerates one by one
            await ProduceNumbersAsync().ForEachAsync(number =>
            {
                Console.WriteLine($"{number}");
            });

            Console.WriteLine("A DONE");
        }

        static async Task B()
        {
            // Waits until all are enumerated
            (await ProduceNumbersAsync().ToListAsync()).ForEach(number =>
            {
                Console.WriteLine($"{number}");
            });

            Console.WriteLine("B DONE");
        }

        static async Task C()
        {
            // Enumerates one by one as parallel.
            await ProduceNumbersAsync().ParallelForEachAsync(number =>
            {
                Console.WriteLine($"{number}");

                return Task.CompletedTask;
            });

            Console.WriteLine("C DONE");
        }

        static IAsyncEnumerable<int> ProduceNumbersAsync()
        {
            return new AsyncEnumerable<int>(async yield => {
                
                for (int i = 0; i < 100; i++)
                {
                    await Task.Delay(10);
                    await yield.ReturnAsync(i);
                }
            });
        }

        static async Task<TimeSpan> Measure(Func<Task> func)
        {
            var stopWatch = Stopwatch.StartNew();

            await func();

            stopWatch.Stop();

            return stopWatch.Elapsed;            
        }

        // Pros:
        //      ParallelAsync 

        // Cons:
        //      yield not used as a compiler keyboard but rather as a type/variable
        //      more verbose    
    }
}
