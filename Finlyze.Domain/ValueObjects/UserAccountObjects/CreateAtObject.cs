namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class CreateAt : ValueObject
{
    public DateTime Value { get; set; }

    public CreateAt()
    {
        Value = DateTime.UtcNow;
    }
}