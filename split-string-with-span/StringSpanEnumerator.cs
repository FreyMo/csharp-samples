using System;
using System.Collections;

namespace split_string_with_span
{
    public ref struct StringSpanEnumerator
    {
        private readonly ReadOnlySpan<char> _source;
        private readonly char _separator;
        private int _start;
        private int _end;
        private int _current;

        public StringSpanEnumerator(ReadOnlySpan<char> source, char separator)
        {
            _source = source;
            _separator = separator;
            _start = 0;
            _end = 0;
            _current = 0;
        }

        public StringSpanEnumerator GetEnumerator() => this;

        public ReadOnlySpan<char> Current => _source[_start.._end];
        
        public bool MoveNext()
        {
            if (_current > _source.Length)
            {
                return false;
            }

            var sliced = _source.Slice(_current);
            var index = sliced.IndexOf(_separator);
            var length = index == -1 ? sliced.Length : index;

            _start =  _current;
            _end = _start + length;

            // Length of 1 char
            _current = _end + 1;

            return true;
        }
    }
}
