namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class Password : ValueObject
{
    public string Value { get; set; }

    public Password(string password)
    {
        Value = password;
    }
}