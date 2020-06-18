using System;
using System.Linq;

namespace event_sourcing
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var account = new BankAccount(-25m);

            for (var i = 0; i < 100; i++)
            {
                account.Transactions.Enqueue(Randomizer.GetRandomTransaction());
            }

            Console.WriteLine("Last 5 transactions:");

            foreach (var balance in account.GetHistory().TakeLast(5))
            {
                Console.WriteLine($"Before: {balance.Before}");
                Console.WriteLine($"After: {balance.After}");
                Console.WriteLine();
            }
            
            Console.WriteLine($"Current balance: {account.GetBalances().Last()}");
            Console.WriteLine($"Maximum balance: {account.GetBalances().Max()}");
            Console.WriteLine($"Minimum balance: {account.GetBalances().Min()}");
        }
    }
}
