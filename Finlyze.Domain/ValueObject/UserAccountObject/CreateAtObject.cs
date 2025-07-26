using Finlyze.Domain.ValueObjects.Base;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class CreateAt : ValueObject
{
    public DateTime Value { get; set; }

    public CreateAt()
    {
        Value = DateTime.UtcNow;
    }

    public CreateAt(DateTime create)
    {
        Value = create;
    }
}