using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class Password : ValueObject
{
    public string Value { get; set; }

    public Password(string password)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(password), "Password não pode ser vazio ou nulo.");
        Value = password;
    }

    private Password() { }
}