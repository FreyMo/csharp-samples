using System;
using System.Text.Json;

namespace record_types
{
    public static class SerializationSample
    {
        // It is finally possible to deserialize into immutable types with System.Text.Json!
        public static void Run()
        {
            var deserialized = JsonSerializer.Deserialize<MyRecord>("{\"MyInt\":3,\"MyString\":\"test\"}");

            Console.WriteLine(deserialized.MyInt);
            Console.WriteLine(deserialized.MyString);
        }
    }
}
