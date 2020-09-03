using System;

namespace span_of_t
{
    class Program
    {
        static void Main(string[] args)
        {
            SliceIt("");
        }
    }

    public static void DoStuff()
    {

    }

    public ReadOnlySpan<char> SliceIt(ReadOnlySpan<char> span)
    {
        var temp = span.Overlaps("");

    }
}
