using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.FinancialTransactionObjects;

public class Amount : ValueObject
{
    public decimal Value { get; private set; }

    public Amount(decimal amount)
    {
        DomainException.ThrowIfFalse(amount != 0, "Amount não pode ser igual a 0.");
        Value = amount;
    }

    private Amount() { }
}