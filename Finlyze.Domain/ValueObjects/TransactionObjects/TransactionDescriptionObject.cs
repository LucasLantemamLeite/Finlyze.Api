namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TransactionDescription : ValueObject
{
    public string? Value { get; private set; }

    public TransactionDescription(string? description)
    {
        Value = description;
    }

    private TransactionDescription() { }
}