using Finlyze.Domain.ValueObjects.Base;

namespace Finlyze.Domain.ValueObjects.FinancialTransactionObjects;

public class UpdateAt : ValueObject
{
    public DateTime Value { get; private set; }

    public UpdateAt()
    {
        Value = DateTime.UtcNow;
    }

    public UpdateAt(DateTime update_at)
    {
        Value = update_at;
    }
}