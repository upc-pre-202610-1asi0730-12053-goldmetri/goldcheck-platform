namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record Temperature
{
    public Temperature() => Celsius = 0m;

    public Temperature(decimal celsius)
    {
        if (celsius < -50m || celsius > 1500m)
            throw new ArgumentException(
                "Temperature must be between -50 °C and 1500 °C.", nameof(celsius));
        Celsius = celsius;
    }

    public decimal Celsius { get; init; }
}