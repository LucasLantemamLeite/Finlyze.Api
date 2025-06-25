namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class Description : ValueObject
{
    public string Value { get; set; }

    public Description(string description)
    {
        Value = description;
    }
}