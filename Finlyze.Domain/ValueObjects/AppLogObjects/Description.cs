using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class Description : ValueObject
{
    public string Value { get; set; }

    public Description(string description)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(description), "Description não pode ser vazio ou nulo.");
        Value = description;
    }
}