namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TransactionDate : ValueObject
{
    public DateTime Value { get; set; }

    public TransactionDate(DateTime? date)
    {
        Value = date ?? DateTime.Now;
    }
}