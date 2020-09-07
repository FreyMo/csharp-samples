using System;

namespace split_string_with_span
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample implementation of a StringSpanEnumerator that allows splitting strings without memory allocation on the heap");
            
            ReadOnlySpan<char> temp = "abc.d.ef";

            var splitString = temp.Split('.');

            foreach (var str in splitString)
            {
                Console.WriteLine(str);
                
            }
        }
    }
}
