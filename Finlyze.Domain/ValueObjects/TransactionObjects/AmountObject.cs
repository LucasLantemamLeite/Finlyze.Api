using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class Amount : ValueObject
{
    public decimal Value { get; set; }

    public Amount(decimal amount)
    {
        DomainException.ThrowIfFalse(amount != 0, "Amount não pode ser igual a 0.");
        Value = amount;
    }

    private Amount() { }
}