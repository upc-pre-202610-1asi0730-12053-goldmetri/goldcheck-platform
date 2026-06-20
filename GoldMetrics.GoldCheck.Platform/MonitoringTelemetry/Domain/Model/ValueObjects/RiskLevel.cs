namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record RiskLevel
{
    private static readonly HashSet<string> AllowedLevels =
        new(StringComparer.OrdinalIgnoreCase) { "Low", "Medium", "High", "Critical" };

    public RiskLevel() => Value = string.Empty;

    public RiskLevel(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RiskLevel cannot be empty.", nameof(value));
        if (!AllowedLevels.Contains(value))
            throw new ArgumentException(
                $"Invalid risk level '{value}'. Allowed: Low, Medium, High, Critical.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
