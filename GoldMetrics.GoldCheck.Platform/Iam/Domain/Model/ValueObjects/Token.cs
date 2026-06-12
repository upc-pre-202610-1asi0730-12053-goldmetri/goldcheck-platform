namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.ValueObjects;

public record Token
{
    public Token() => Value = string.Empty;
    public Token(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Token cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}

