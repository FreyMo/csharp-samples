using System;

namespace split_string_with_span
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample implementation of a StringSpanEnumerator that allows splitting strings without memory allocation on the heap");
            
            ReadOnlySpan<char> temp = "abc.d.ef";
            Show(temp);
            Show("tester".AsSpan());
            Show(",,.,.,.,,,,,.,.");
        }

        private static void Show(ReadOnlySpan<char> source)
        {
            Console.WriteLine($"Original: {source.ToString()}");
            Console.WriteLine($"Split:");

            foreach (var slice in source.Split('.'))
            {
                Console.WriteLine(slice.ToString());
            }
        }
    }
}
