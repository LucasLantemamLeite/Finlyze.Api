using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class Description : ValueObject
{
    public string Value { get; private set; }

    public Description(string description)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(description), "Description não pode ser vazio ou nulo.");
        Value = description;
    }

    private Description() { }
}