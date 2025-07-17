namespace Finlyze.Domain.ValueObject.TransactionObjects;

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