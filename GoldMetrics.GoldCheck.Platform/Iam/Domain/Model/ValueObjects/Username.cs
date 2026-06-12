namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.ValueObjects;

public record Username
{
    public Username() => Value = string.Empty;
    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username cannot be empty.", nameof(value));
        if (value.Length < 3)
            throw new ArgumentException("Username must be at least 3 characters.", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("Username must be at most 50 characters.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}