using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace system_linq_async
{
    // Get a lot of work done the old fashioned way
    class MultipleTasksParallel
    {
        static async Task DoAsync()
        {
            EnumerateMultiple().AsParallel()
                               .WithDegreeOfParallelism(4)
                               .ForAll(async task =>
            {
                var number = await task;

                // Do something after it has finished;
            });

            Console.WriteLine("All Done!");
        }

        static IEnumerable<Task<int>> EnumerateMultiple()
        {
            yield return Task.Run(() => 0);
            yield return Task.Run(() => 1);
            yield return Task.Run(() => 2);
        }
    }
}
