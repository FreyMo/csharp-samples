namespace span_of_t
{
    class Program
    {
        static void Main(string[] args)
        {
            Slicing.Run();
            MemoryAndOwnership.Run();
            UnmanagedHeap.Run();
            StackAndHeap.Run();
            RefStructs.Run();
            UsageOfMySpan.Run();
        }
    }
}
