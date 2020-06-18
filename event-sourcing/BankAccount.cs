using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace event_sourcing
{
    public class BankAccount
    {
        private readonly decimal _startingBalance = -25;

        public BankAccount(decimal startingBalance)
        {
            _startingBalance = startingBalance;
        }
        
        // Note that a ConcurrentQueue is not the best list type as it is not read only and items can be dequeued.
        public ConcurrentQueue<ITransaction> Transactions { get; } = new ConcurrentQueue<ITransaction>();

        public IEnumerable<BalanceItem> GetHistory()
        {
            var balanceBefore = GetBalances().First();

            foreach (var balance in GetBalances().Skip(1))
            {
                yield return new BalanceItem
                {
                    Before = balanceBefore,
                    After = balance
                };

                balanceBefore = balance;
            }
        }

        public IEnumerable<decimal> GetBalances()
        {
            yield return _startingBalance;

            var currentBalance = _startingBalance;

            foreach (var transaction in Transactions)
            {
                currentBalance = transaction.Modify(currentBalance);

                yield return currentBalance;
            }
        }
    }
}
