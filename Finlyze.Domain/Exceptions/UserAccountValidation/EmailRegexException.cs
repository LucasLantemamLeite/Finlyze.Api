using System.Text.RegularExpressions;

namespace Finlyze.Domain.ValueObject.Validation;

public partial class EmailRegexException : Exception
{
    public EmailRegexException(string message) : base(message) { }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();

    public static void ThrowIfNotMatch(string email, string message)
    {
        if (!EmailRegex().IsMatch(email))
            throw new EmailRegexException(message);
    }
}