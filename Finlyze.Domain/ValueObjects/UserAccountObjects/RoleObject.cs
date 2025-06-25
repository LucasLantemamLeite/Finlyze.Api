using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.UserAccountObject;

public class Role : ValueObject
{
    public ERole Value { get; set; }

    public Role(int role)
    {
        RoleException.ThrowIfNotDefined<ERole>(role, "Role Inválido.");
        Value = (ERole)role;
    }
}