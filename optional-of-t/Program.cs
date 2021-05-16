using Optional;
using System;
using System.Linq;

namespace optional_of_t
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new ItemApi(new());
            
            api.GetItem(99).Match(
                some => Console.WriteLine($"Got an item: {some}"),
                () => Console.WriteLine("Oops, we missed!")
            );

            foreach (var item in api.GetItems(100))
            {
                Console.WriteLine(item);
            }

            var optionals = new Option<string>[]
            {
                "test".Some(),
                "test".None()
            };

            foreach (var item in optionals.SelectMany(o => o.ToEnumerable()))
            {
                Console.WriteLine(item);
            }
        }
    }
}
