namespace Finlyze.Domain.ValueObjects.Enums;

[Flags]
public enum ERole
{
    User = 1 << 0,
    Admin = 1 << 1
}