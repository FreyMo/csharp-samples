namespace event_sourcing
{
    public struct HistoryItem
    {
        public decimal BalanceBefore { get; set; }

        public decimal BalanceAfter { get; set; }
    }
}
