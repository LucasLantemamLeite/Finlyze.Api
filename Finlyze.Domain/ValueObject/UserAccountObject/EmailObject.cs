using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class Email : ValueObject
{
    public string Value { get; set; }

    public Email(string email)
    {
        EmailRegexException.ThrowIfNotMatch(email, "Email Inválido.");
        Value = email.ToLower();
    }

    private Email() { }
}