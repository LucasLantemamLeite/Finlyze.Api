using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.SystemLogObjects;

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