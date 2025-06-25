namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class Amount : ValueObject
{
    public decimal Value { get; set; }

    public Amount(decimal? amount)
    {
        Value = amount ?? 0;
    }
}