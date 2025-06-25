namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class PhoneNumber : ValueObject
{
    public string Value { get; set; }

    public PhoneNumber(string phone)
    {
        Value = phone;
    }
}