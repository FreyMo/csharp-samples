using System;

namespace span_of_t
{
    public static class UsageOfMySpan
    {
        public static void Run()
        {
            // implicit cast from array
            MySpan<int> mySpan = new[] {1, 2, 3};

            // explicit constructor call
            MySpan<int> myOtherSpan = new MySpan<int>(new[] {1, 2, 3, 4}, start: 1, length: 2);

            var array = mySpan.ToArray();

            // GetEnumerator implementation
            foreach (var item in mySpan)
            {
                // Writes 1, 2, 3
                Console.WriteLine(item);
            }

            // Access Range indexer
            foreach (var item in myOtherSpan[..])
            {
                // Writes 2 and 3
                Console.WriteLine(item);
            }
        }
    }
}