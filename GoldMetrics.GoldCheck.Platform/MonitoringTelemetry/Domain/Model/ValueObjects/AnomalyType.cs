namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record AnomalyType
{
    private static readonly HashSet<string> AllowedTypes =
        new(StringComparer.OrdinalIgnoreCase)
            { "Temperature", "Communication", "GNSS", "Speed", "Pressure" };

    public AnomalyType() => Value = string.Empty;

    public AnomalyType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AnomalyType cannot be empty.", nameof(value));
        if (!AllowedTypes.Contains(value))
            throw new ArgumentException(
                $"Invalid anomaly type '{value}'. Allowed: Temperature, Communication, GNSS, Speed, Pressure.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}