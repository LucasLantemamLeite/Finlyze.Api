namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class UpdateAt : ValueObject
{
    public DateTime? Value { get; set; }

    public UpdateAt(DateTime? date)
    {
        Value = date;
    }
}