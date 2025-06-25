using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class PhoneNumber : ValueObject
{
    public string Value { get; set; }

    public PhoneNumber(string phone)
    {
        PhoneNumberRegexException.ThrowIfNotMatch(phone, "PhoneNumber Inválido.");
        Value = phone;
    }
}