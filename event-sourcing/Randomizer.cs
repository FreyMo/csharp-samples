using System;

namespace event_sourcing
{
    public static class Randomizer
    {
        private static readonly Random _random = new Random();

        public static ITransaction CreateTransaction()
        {
            var amount = (decimal)_random.NextDouble() * 50 - 25;

            var isIncoming = amount >= 0;

            return isIncoming switch
            {
                true => new IncomingPayment(amount),
                false => new OutgoingPayment(Math.Abs(amount))
            };
        }
    }
}
