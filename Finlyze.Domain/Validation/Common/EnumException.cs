namespace Finlyze.Domain.ValueObjects.Validations.Exceptions;

public class EnumException : Exception
{
    public EnumException(string message) : base(message) { }

    public static void ThrowIfNotDefined<TEnum>(int type, string message) where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), type))
            throw new EnumException(message);
    }
}