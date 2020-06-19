using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace event_sourcing
{
    public class BankAccount
    {
        private readonly decimal _startingBalance;
        private readonly ConcurrentQueue<ITransaction> _transactions = new ConcurrentQueue<ITransaction>();

        public BankAccount(decimal startingBalance)
        {
            _startingBalance = startingBalance;
        }
        
        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Enqueue(transaction ?? throw new ArgumentNullException());
        }

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

            foreach (var transaction in _transactions)
            {
                currentBalance = transaction.Modify(currentBalance);

                yield return currentBalance;
            }
        }
    }
}
