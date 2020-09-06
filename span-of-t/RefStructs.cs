using System;

namespace span_of_t
{
    public static class RefStructs
    {
        public static void Run()
        {
            Console.WriteLine("This demonstrates the use of ref structs.");

            var myStruct = new MyStruct();
            var myRefStruct = new MyRefStruct()
            {
                AnotherRefStruct = new AnotherRefStruct
                {
                    MyProperty = 123
                }
            };

            object boxedStruct = (object)myStruct;
            // Boxing is not possible for ref structs!
            // object boxedRefStruct = (object)myRefStruct;

            // can be passed around and returned
            var result = DoSomethingWithMyRefStruct(myRefStruct);
        }

        public static MyRefStruct DoSomethingWithMyRefStruct(MyRefStruct refStruct)
        {
            Console.WriteLine($"Content: {refStruct.AnotherRefStruct.MyProperty}");

            return refStruct;
        }
    }

    public class MyClass
    {
        public MyStruct MyStruct { get; init; }

        // Won't compile, because ref structs cannot be members of classes
        // That's why they can't be passed to lambda or async functions (as a display class gets generated)
        // public MyRefStruct MyRefStruct { get; init; }
    }

    public struct MyStruct
    {
        public int A { get; init; }

        // Won't compile, because ref structs cannot be members of structs
        // public MyRefStruct MyRefStruct { get; init; }
    }

    public ref struct MyRefStruct
    {
        public MyClass MyClass { get; init; }

        // This is possible, because MyRefStruct is a ref struct.
        // If it were a class or a struct, it would not be possible.
        public AnotherRefStruct AnotherRefStruct { get; init; }
    }

    public ref struct AnotherRefStruct
    {
        public int MyProperty { get; init; }
    } 
}
