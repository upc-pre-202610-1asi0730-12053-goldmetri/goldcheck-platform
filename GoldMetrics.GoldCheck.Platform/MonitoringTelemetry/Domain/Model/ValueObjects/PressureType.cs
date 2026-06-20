namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record PressureType
{
    private static readonly HashSet<string> AllowedTypes =
        new(StringComparer.OrdinalIgnoreCase)
            { "OilFilterDifference", "OilPan", "AbsoluteEngineOil", "OilFilter" };

    public PressureType() => Value = string.Empty;

    public PressureType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("PressureType cannot be empty.", nameof(value));
        if (!AllowedTypes.Contains(value))
            throw new ArgumentException(
                $"Invalid pressure type '{value}'. Allowed: OilFilterDifference, OilPan, AbsoluteEngineOil, OilFilter.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}