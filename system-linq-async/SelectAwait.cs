using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace system_linq_async
{
    // Use Select await for asynchronous select
    class SelectAwait
    {
        static async Task DoAsync()
        {
            // As lambda
            var selected = EnumerateAsync().SelectAwait(async number =>
            {
                await Task.Delay(1);
                return number + 1;
            });

            await foreach (var number in selected)
            {
                // ...
            }

            // As method group
            EnumerateAsync().SelectAwait(ModifyDelayed);
        }

        static async IAsyncEnumerable<int> EnumerateAsync()
        {
            yield return 0;

            await Task.Delay(TimeSpan.FromSeconds(1));

            yield return 1;
        }

        static async ValueTask<int> ModifyDelayed(int number)
        {
            await Task.Delay(1);

            return number + 1;
        }
    }
}
