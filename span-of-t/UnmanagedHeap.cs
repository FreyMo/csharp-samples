using System;
using System.Runtime.InteropServices;

namespace span_of_t
{
    public static class UnmanagedHeap
    {
        public static void Run()
        {
            Console.WriteLine("This allocated memory on the unmanaged heap.");

            var numbers = new[] {1, 2, 3};
            IntPtr ptr = Marshal.AllocHGlobal(numbers.Length * sizeof(int));
            try
            {
                Span<int> numbersSpan;

                unsafe
                {
                    numbersSpan = new Span<int>((int*)ptr, numbers.Length);
                }

                numbers.CopyTo(numbersSpan);

                foreach (var item in numbersSpan)
                {
                    Console.WriteLine(item);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}