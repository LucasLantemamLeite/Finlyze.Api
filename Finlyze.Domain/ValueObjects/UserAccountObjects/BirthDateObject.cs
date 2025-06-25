namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class BirthDate : ValueObject
{
    public DateOnly Value { get; set; }

    public BirthDate(DateOnly birth)
    {
        Value = birth;
    }
}