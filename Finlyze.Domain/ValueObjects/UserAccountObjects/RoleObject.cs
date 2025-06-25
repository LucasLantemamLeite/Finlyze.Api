using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class Role : ValueObject
{
    public ERole Value { get; set; }

    public Role(int role)
    {
        Value = (ERole)role;
    }
}