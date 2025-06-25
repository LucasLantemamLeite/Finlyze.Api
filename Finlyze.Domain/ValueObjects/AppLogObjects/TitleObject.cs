namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class Title : ValueObject
{
    public string Value { get; set; }

    public Title(string title)
    {
        Value = title;
    }
}