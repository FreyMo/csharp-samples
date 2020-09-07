using System;

namespace split_string_with_span
{
    public static class ReadOnlySpanOfCharExtensions
    {
        public static StringSpanEnumerator Split(this ReadOnlySpan<char> source, char separator)
        {
            return new StringSpanEnumerator(source, separator);
        }
    }
}
