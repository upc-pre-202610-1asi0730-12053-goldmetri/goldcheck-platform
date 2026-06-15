using System.Text.RegularExpressions;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

public record Language
{
    private static readonly Regex Iso6391 =
        new(@"^[a-z]{2}$", RegexOptions.Compiled);

    public Language() => Code = string.Empty;

    public Language(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Language code cannot be empty.", nameof(code));
        if (!Iso6391.IsMatch(code))
            throw new ArgumentException(
                "Language code must be a valid ISO 639-1 code (two lowercase letters, e.g. 'en', 'es').",
                nameof(code));
        Code = code;
    }

    public string Code { get; init; }
}