using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class Name : ValueObject
{
    public string Value { get; set; }

    public Name(string name)
    {
        DomainException.ThrowIfFalse(!string.IsNullOrWhiteSpace(name), "Name não pode ser vazio ou nulo.");
        Value = name;
    }

    private Name() { }
}