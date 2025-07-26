using Finlyze.Domain.ValueObjects.Base;

namespace Finlyze.Domain.ValueObjects.SystemLogObjects;

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