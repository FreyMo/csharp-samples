namespace event_sourcing
{
    public class HistoryItem
    {
        public decimal BalanceBefore { get; set; }

        public decimal BalanceAfter { get; set; }
    }
}
