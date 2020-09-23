using System;

namespace record_types
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationSample.Run();
            Demonstrate();
        }

        private static void Demonstrate()
        {
            var rec = new MyRecord
            {
                MyInt = 3,
                MyString = ""
            };

            var rec2 = rec with { };
            var rec3 = rec with { MyInt = 4 };
            // rec.MyInt = 4; Won't compile!

            Console.WriteLine(rec.MyInt);
            Console.WriteLine(rec2.MyInt);
            Console.WriteLine(rec3.MyInt);
            Console.WriteLine(rec2 == rec);
            Console.WriteLine(rec3 == rec);

            var (myInt, myString) = rec;

            Console.WriteLine(myInt);
            Console.WriteLine(myString);
        }
    }
}
