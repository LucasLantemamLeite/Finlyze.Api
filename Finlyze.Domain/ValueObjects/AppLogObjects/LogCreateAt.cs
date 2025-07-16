namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class LogCreateAt : ValueObject
{
    public DateTime Value { get; private set; }

    public LogCreateAt()
    {
        Value = DateTime.UtcNow;
    }

    public LogCreateAt(DateTime create_at)
    {
        Value = create_at;
    }
}