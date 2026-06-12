namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.ValueObjects;

public record HashedPassword
{
    public HashedPassword() => Value = string.Empty;
    public HashedPassword(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("HashedPassword cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}