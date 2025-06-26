using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class LogTitle : ValueObject
{
    public string Value { get; set; }

    public LogTitle(string title)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(title), "Title não pode ser vazio ou nulo.");
        Value = title;
    }

    private LogTitle() { }
}