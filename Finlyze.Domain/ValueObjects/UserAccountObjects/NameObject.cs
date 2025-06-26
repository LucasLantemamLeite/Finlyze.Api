using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.UserAccountObject;

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