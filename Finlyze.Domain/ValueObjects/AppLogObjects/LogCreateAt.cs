namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class LogCreateAt : ValueObject
{
    public DateTime Value { get; set; }

    public LogCreateAt()
    {
        Value = DateTime.UtcNow;
    }
}