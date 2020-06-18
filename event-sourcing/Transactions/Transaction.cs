namespace event_sourcing
{
    public interface ITransaction
    {
        decimal Modify(decimal balance);
    }
}
