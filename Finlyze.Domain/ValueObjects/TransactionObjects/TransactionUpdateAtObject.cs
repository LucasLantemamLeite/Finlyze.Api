namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TransactionUpdateAt : ValueObject
{
    public DateTime Value { get; set; }

    public TransactionUpdateAt()
    {
        Value = DateTime.UtcNow;
    }
}