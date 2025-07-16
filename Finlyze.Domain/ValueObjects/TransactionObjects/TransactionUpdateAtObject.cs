namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TransactionUpdateAt : ValueObject
{
    public DateTime Value { get; private set; }

    public TransactionUpdateAt()
    {
        Value = DateTime.UtcNow;
    }

    public TransactionUpdateAt(DateTime update_at)
    {
        Value = update_at;
    }
}