using System;
using System.Buffers;

namespace span_of_t
{
    public static class MemoryAndOwnership
    {
        public static void Run()
        {
            using (IMemoryOwner<char> owner = MemoryPool<char>.Shared.Rent())
            {
                Console.WriteLine("Enter a number: ");

                // Argument checking omitted for brevity.
                var value = Int32.Parse(Console.ReadLine());
                var memory = owner.Memory;

                WriteToBuffer(value, memory);
                DisplayBuffer(memory);
            }
        }

        private static void WriteToBuffer(int value, Memory<char> buffer)
        {
            var str = value.ToString();

            // Memory gets used to pass around a wrapper for Span on the heap. Access .Span to use the span on the stack
            var span = buffer.Span;

            for (var i = 0; i < str.Length; i++)
            {
                span[i] = str[i];
            }

            // Better:
            value.ToString().AsSpan().CopyTo(span);
        }

        private static void DisplayBuffer(ReadOnlyMemory<char> memory)
        {
            Console.WriteLine(memory);
        }
    }
}
