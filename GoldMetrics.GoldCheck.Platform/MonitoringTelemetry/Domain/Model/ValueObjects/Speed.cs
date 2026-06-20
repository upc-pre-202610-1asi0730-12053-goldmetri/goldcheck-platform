namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record Speed
{
    public Speed() => KmPerHour = 0m;

    public Speed(decimal kmPerHour)
    {
        if (kmPerHour < 0m)
            throw new ArgumentException(
                "Speed must be greater than or equal to 0 km/h.", nameof(kmPerHour));
        KmPerHour = kmPerHour;
    }

    public decimal KmPerHour { get; init; }
}