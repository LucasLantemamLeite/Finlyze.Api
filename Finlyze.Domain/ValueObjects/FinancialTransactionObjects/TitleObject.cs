using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class Title : ValueObject
{
    public string Value { get; private set; }

    public Title(string title)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(title), "Title não pode ser vazio ou nulo.");
        Value = title;
    }

    private Title() { }
}