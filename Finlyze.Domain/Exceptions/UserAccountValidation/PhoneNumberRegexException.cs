using System.Text.RegularExpressions;

namespace Finlyze.Domain.ValueObject.Validation;

public partial class PhoneNumberRegexException : Exception
{
    public PhoneNumberRegexException(string message) : base(message) { }

    [GeneratedRegex(@"^\+\d{8,15}$")]
    private static partial Regex PhoneNumberRegex();

    public static void ThrowIfNotMatch(string phone, string message)
    {
        if (!PhoneNumberRegex().IsMatch(phone))
            throw new PhoneNumberRegexException(message);
    }
}