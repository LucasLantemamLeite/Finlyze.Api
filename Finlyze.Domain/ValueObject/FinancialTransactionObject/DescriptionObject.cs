using Finlyze.Domain.ValueObjects.Base;

namespace Finlyze.Domain.ValueObjects.FinancialTransactionObjects;

public class Description : ValueObject
{
    public string? Value { get; private set; }

    public Description(string? description)
    {
        Value = description;
    }

    private Description() { }
}