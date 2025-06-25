using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class Title : ValueObject
{
    public string Value { get; set; }

    public Title(string title)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(title), "Title não pode ser vazio ou nulo.");
        Value = title;
    }
}