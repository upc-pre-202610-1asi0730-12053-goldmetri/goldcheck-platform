namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.ValueObjects;

public record RiskLevel
{
    private static readonly string[] AllowedValues = ["Low", "Medium", "High", "Critical"];

    public RiskLevel() => Value = string.Empty;

    public RiskLevel(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RiskLevel cannot be empty.", nameof(value));
        if (!AllowedValues.Contains(value))
            throw new ArgumentException($"RiskLevel must be one of: {string.Join(", ", AllowedValues)}.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}