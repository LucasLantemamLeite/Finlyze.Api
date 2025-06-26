namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TransactionCreateAt : ValueObject
{
    public DateTime Value { get; set; }

    public TransactionCreateAt(DateTime? date)
    {
        Value = date ?? DateTime.Now;
    }

    private TransactionCreateAt() { }
}