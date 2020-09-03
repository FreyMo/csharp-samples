using System;

namespace record_types
{
    class Program
    {
        static void Main(string[] args)
        {
            var rec = new MyRecord
            {
                MyInt = 3,
                MyString = ""
            };

            var anotherRecord = rec with { MyInt = 4 };
            // rec.MyInt = 4;

            Console.WriteLine(rec.MyInt);
            Console.WriteLine(anotherRecord.MyInt);
            Console.WriteLine(anotherRecord == rec);
        }
    }







    public record MyRecord
    {
        public int MyInt { get; init; }
        public string MyString { get; init; }
    }
}
