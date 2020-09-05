using System;

namespace span_of_t
{
    public static class Slicing
    {
        public static void Run()
        {
            var str = "abcdefg";
            
            // abc
            Console.WriteLine(str[..3]);

            // efg
            Console.WriteLine(str[^3..]);

            // bcdef
            Console.WriteLine(str[1..^1]);

            // explicit
            var range = new Range(new Index(0, false), new Index(0, true));
            Console.WriteLine(str[range]);
        }
    }

}
