using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace event_sourcing
{
    public class BankAccount
    {
        private readonly decimal _startingBalance;
        private readonly ConcurrentQueue<ITransaction> _transactions = new ConcurrentQueue<ITransaction>();
        private readonly BehaviorSubject<decimal> _currentBalance;

        public BankAccount(decimal startingBalance)
        {
            _startingBalance = startingBalance;

            _currentBalance = new BehaviorSubject<decimal>(_startingBalance);
        }
        
        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Enqueue(transaction ?? throw new ArgumentNullException());

            _currentBalance.OnNext(GetBalances().Last());
        }

        public IObservable<decimal> GetCurrentBalance() => _currentBalance.AsObservable();

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
