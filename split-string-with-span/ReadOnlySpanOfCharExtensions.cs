using System;

namespace split_string_with_span
{
    public static class ReadOnlySpanOfCharExtensions
    {
        public static StringSpanEnumerator Split(this ReadOnlySpan<char> stringSpan, char c)
        {
            return new StringSpanEnumerator(stringSpan, c);
        }
    }
}
