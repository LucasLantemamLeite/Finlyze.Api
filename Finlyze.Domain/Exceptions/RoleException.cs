namespace Finlyze.Domain.ValueObject.Validation;

public class RoleException : Exception
{
    public RoleException(string message) : base(message) { }

    public static void ThrowIfNotDefined<TEnum>(int type, string message) where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), type))
            throw new RoleException(message);
    }
}