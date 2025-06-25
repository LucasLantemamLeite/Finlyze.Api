namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class Name : ValueObject
{
    public string Value { get; set; }

    public Name(string name)
    {
        Value = name;
    }
}