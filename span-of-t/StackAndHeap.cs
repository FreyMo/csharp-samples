using System;

namespace span_of_t
{
    public static class StackAndHeap
    {
        public static void Run()
        {
            Console.WriteLine("Enter an array size:");

            // Argument checking omitted for brevity
            var size = Int32.Parse(Console.ReadLine());
            
            // Stack size is limited.
            if (size >= 1000)
            {
                AllocateOnHeap();
            }
            else
            {
                AllocateOnStack();
            }
        }

        private static void AllocateOnStack()
        {
            // Won't compile, because var becomes an int pointer:
            // var ints = stackalloc[] {1, 2, 3}; Only allowed in an unsafe context

            // Compiles because ReadOnlySpan is explicitily set as a type.
            ReadOnlySpan<int> ints = stackalloc[] {1, 2, 3}; 
            
            Console.WriteLine("I was allocated on the stack");

            // Returning ints is impossible, because they only live on the stack, which gets popped when leaving this scope:
            // return temp;
        }

        private static void AllocateOnHeap()
        {
            Console.WriteLine("I was allocated on the heap");

            var ints = new[] {1, 2, 3};
        }
    }
}
