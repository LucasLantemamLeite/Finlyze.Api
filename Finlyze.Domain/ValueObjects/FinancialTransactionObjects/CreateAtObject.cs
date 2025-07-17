namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class CreateAt : ValueObject
{
    public DateTime Value { get; private set; }

    public CreateAt(DateTime? date)
    {
        Value = date ?? DateTime.Now;
    }

    private CreateAt() { }
}