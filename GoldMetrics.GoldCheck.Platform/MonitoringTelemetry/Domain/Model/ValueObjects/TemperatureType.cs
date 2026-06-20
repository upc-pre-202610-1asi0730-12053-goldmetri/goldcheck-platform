namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record TemperatureType
{
    private static readonly HashSet<string> AllowedTypes =
        new(StringComparer.OrdinalIgnoreCase)
            { "Exhaust", "Refrigerant", "EngineOil", "Fuel" };

    public TemperatureType() => Value = string.Empty;

    public TemperatureType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("TemperatureType cannot be empty.", nameof(value));
        if (!AllowedTypes.Contains(value))
            throw new ArgumentException(
                $"Invalid temperature type '{value}'. Allowed: Exhaust, Refrigerant, EngineOil, Fuel.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}