using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Domain.ValueObject.Validation;

public class ERoleException : Exception
{
    public ERoleException(string message) : base(message) { }

    public static void ThrowIfNotDefined(int role, string message)
    {
        if (!Enum.IsDefined(typeof(ERole), role))
            throw new ERoleException(message);
    }
}