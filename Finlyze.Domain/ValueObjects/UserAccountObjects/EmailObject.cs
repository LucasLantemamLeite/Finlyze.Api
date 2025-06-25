using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class Email : ValueObject
{
    public string Value { get; set; }

    public Email(string email)
    {
        EmailRegexException.ThrowIfNotMatch(email, "Email Inválido.");
        Value = email.ToLower();
    }
}