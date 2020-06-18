using System;

namespace event_sourcing
{
    public class OutgoingPayment : ITransaction
    {
        private readonly decimal _amount;

        public OutgoingPayment(decimal amount)
        {
            _amount = amount >= 0 ? amount : throw new ArgumentOutOfRangeException();
        }

        public decimal Modify(decimal balance) => balance - _amount;
    }
}
