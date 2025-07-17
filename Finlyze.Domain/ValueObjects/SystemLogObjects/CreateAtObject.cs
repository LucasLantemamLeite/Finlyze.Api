namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class CreateAt : ValueObject
{
    public DateTime Value { get; private set; }

    public CreateAt()
    {
        Value = DateTime.UtcNow;
    }

    public CreateAt(DateTime create_at)
    {
        Value = create_at;
    }
}