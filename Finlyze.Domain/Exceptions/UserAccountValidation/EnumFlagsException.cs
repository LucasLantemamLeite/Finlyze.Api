namespace Finlyze.Domain.ValueObject.Validation;

public class EnumFlagsException : Exception
{
    public EnumFlagsException(string message) : base(message) { }

    public static void ThrowIfNotFlagDefined<TEnum>(int type, string message) where TEnum : struct, Enum
    {
        var allDefinedValues = Enum.GetValues<TEnum>().Cast<int>().Aggregate((a, b) => a | b);

        if ((type & ~allDefinedValues) != 0)
            throw new EnumFlagsException(message);
    }
}