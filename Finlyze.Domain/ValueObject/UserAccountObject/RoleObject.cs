using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Enums;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class Role : ValueObject
{
    public ERole Value { get; set; }

    public Role(int role)
    {
        EnumFlagsException.ThrowIfNotFlagDefined<ERole>(role, "Role Inválido.");
        Value = (ERole)role;
    }

    private Role() { }
}