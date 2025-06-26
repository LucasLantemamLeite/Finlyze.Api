namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TransactionDescription : ValueObject
{
    public string? Value { get; set; }

    public TransactionDescription(string? description)
    {
        Value = description;
    }

    private TransactionDescription() { }
}