using System;

namespace event_sourcing
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var account = new BankAccount(0);

            for (var i = 0; i < 10; i++)
            {
                account.Transactions.Enqueue(new IncomingPayment(5));
            }

            for (var i = 0; i < 15; i++)
            {
                account.Transactions.Enqueue(new OutgoingPayment(3));
            }

            Console.WriteLine("History:");

            foreach (var historyItem in account.GetHistory())
            {
                Console.WriteLine($"Before: {historyItem.BalanceBefore}");
                Console.WriteLine($"After: {historyItem.BalanceAfter}");
                Console.WriteLine();
            }
            
            Console.WriteLine($"Current balance: {account.GetCurrentBalance()}");
            Console.WriteLine($"Maximum balance: {account.GetMaxBalance()}");
        }
    }
}
