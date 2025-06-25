namespace Finlyze.Domain.ValueObject.Validation;

public class BirthDateException : Exception
{
    public BirthDateException(string message) : base(message) { }

    public static void ThrowIfFalse(DateOnly birth, string message_future, string message_age)
    {
        if (birth > DateOnly.FromDateTime(DateTime.Today))
            throw new BirthDateException(message_future);

        if (birth.AddYears(18) > DateOnly.FromDateTime(DateTime.Today))
            throw new BirthDateException(message_age);
    }
}