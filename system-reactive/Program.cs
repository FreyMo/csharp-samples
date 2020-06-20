
using System;
using System.Linq;
using System.Reactive.Linq;

namespace system_reactive
{
    class Program
    {
        static void Main(string[] args)
        {
            var observable = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);

            using (observable.Average().Subscribe(avg => Console.WriteLine(avg)))
            {
                Console.WriteLine("Press any key to unsubscribe");
                Console.ReadKey();
                
                observable.Wait();
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
