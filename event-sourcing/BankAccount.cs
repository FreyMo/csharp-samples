using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace event_sourcing
{
    public class BankAccount
    {
        private readonly decimal _startingBalance;

        public BankAccount(decimal startingBalance)
        {
            _startingBalance = startingBalance;
        }
        
        public ConcurrentQueue<ITransaction> Transactions { get; } = new ConcurrentQueue<ITransaction>();

        public decimal GetCurrentBalance()
        {
            return Transactions.AsParallel()
                               .Aggregate(_startingBalance, (balance, transaction) => transaction.Modify(balance));
        }

        public decimal GetMaxBalance()
        {
            var maxBalance = _startingBalance;

            Transactions.Aggregate(_startingBalance, (balance, transaction) => 
            {
                var transformed = transaction.Modify(balance);

                if (transformed > maxBalance)
                {
                    maxBalance = transformed;
                }

                return transformed;
            });

            return maxBalance;
        }
        
        public IEnumerable<HistoryItem> GetHistory()
        {
            var currentBalance = _startingBalance;

            foreach (var transaction in Transactions)
            {
                var balanceBefore = currentBalance;

                currentBalance = transaction.Modify(currentBalance);

                yield return new HistoryItem
                {
                    BalanceBefore = balanceBefore,
                    BalanceAfter = currentBalance
                };
            }
        }
    }
}
