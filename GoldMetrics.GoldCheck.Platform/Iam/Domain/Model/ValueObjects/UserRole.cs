namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.ValueObjects;

public record UserRole
{
    private static readonly string[] AllowedValues = ["Operator", "Supervisor", "Administrator", "Jeweler", "Consumer"];
    public UserRole() => Value = string.Empty;
    public UserRole(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("UserRole cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"UserRole must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}

