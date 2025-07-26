using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class PhoneNumber : ValueObject
{
    public string Value { get; set; }

    public PhoneNumber(string phone)
    {
        PhoneNumberRegexException.ThrowIfNotMatch(phone, "PhoneNumber Inválido.");
        Value = phone;
    }

    private PhoneNumber() { }
}