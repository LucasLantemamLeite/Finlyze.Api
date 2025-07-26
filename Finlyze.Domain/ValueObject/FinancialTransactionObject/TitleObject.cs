using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.FinancialTransactionObjects;

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