using Finlyze.Domain.ValueObjects.Base;

namespace Finlyze.Domain.ValueObjects.FinancialTransactionObjects;

public class CreateAt : ValueObject
{
    public DateTime Value { get; private set; }

    public CreateAt(DateTime? date)
    {
        Value = date ?? DateTime.Now;
    }

    private CreateAt() { }
}