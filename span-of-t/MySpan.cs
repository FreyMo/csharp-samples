using System;
using System.Collections;

namespace span_of_t
{
    // Naive sample implementation of Span<T> to understand how it works.
    // https://www.youtube.com/watch?v=8d0PlpQm10w&ab_channel=RainerStropek
    public ref struct MySpan<T>
    {
        private readonly T[] _array;
        private readonly int _start;
        private readonly int _length;

        public MySpan(T[] array) : this(array, 0, array.Length)
        {
        }

        public MySpan(T[] array, int start, int length) => (_array, _start, _length) = (array, start, length);

        public IEnumerator GetEnumerator() => _array.GetEnumerator();

        // ref returns is important here, as it allows value types to be returned as a reference and thus be mutated.
        public ref T this[int index]
        {
            get => ref _array[_start + index];
        }

        public ref T this[Index index]
        {
            get => ref _array[_start + index.GetOffset(_length)];
        }

        public MySpan<T> this[Range range]
        {
            get
            {
                var startRelativeToArray = range.Start.GetOffset(_length);
                var endRelativeToArray = range.End.GetOffset(_length);

                return new MySpan<T>(
                    _array,
                    startRelativeToArray,
                    endRelativeToArray - startRelativeToArray
                );
            }
        }

        public static implicit operator MySpan<T>(T[] array) => new MySpan<T>(array);

        public T[] ToArray()
        {
            var result = new T[_length];
            Array.Copy(_array, _start, result, 0, _length);
            return result;
        }
    }
}