using System;
using System.Collections;

namespace split_string_with_span
{
    public readonly ref struct StringSpanEnumerator
    {
        private readonly ReadOnlySpan<char> _stringSpan;
        private readonly char _c;
        private ReadOnlySpan<char> _current;

        public StringSpanEnumerator(ReadOnlySpan<char> stringSpan, char c)
        {
            _stringSpan = stringSpan;
            _c = c;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return (IEnumerator<char>)this;
        }

        public ReadOnlySpan<char> Current
        {
            get
            {
                if (_current == null)
                {

                }
            }
        }

        public bool MoveNext()
        {
            if (_current == null)
            {
                var startIndex = 0;
                var currentIndex = 0;

                while (true)
                {
                    if (_stringSpan[currentIndex] == '.')
                    {
                        // Move next
                        Current = _stringSpan[startIndex..currentIndex];
                        
                        return IsEnd();
                    }

                    currentIndex++;
                }
            }
        }

        private bool IsEnd()
        {
            // implement me.
        }
    }
}
