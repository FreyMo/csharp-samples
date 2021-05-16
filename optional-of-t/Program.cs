using Optional;
using System;
using System.Linq;

namespace optional_of_t
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program showcases multiple usages of the Optional<T> type for a more functional programming style");
            
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
