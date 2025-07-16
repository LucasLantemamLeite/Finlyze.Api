namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class Description : ValueObject
{
    public string? Value { get; private set; }

    public Description(string? description)
    {
        Value = description;
    }

    private Description() { }
}