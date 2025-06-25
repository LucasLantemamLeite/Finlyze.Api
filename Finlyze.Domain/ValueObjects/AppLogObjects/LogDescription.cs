using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class LogDescription : ValueObject
{
    public string Value { get; set; }

    public LogDescription(string description)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(description), "Description não pode ser vazio ou nulo.");
        Value = description;
    }
}