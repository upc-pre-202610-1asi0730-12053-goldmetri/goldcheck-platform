namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record GNSSCoordinate
{
    public GNSSCoordinate() { Latitude = 0d; Longitude = 0d; }

    public GNSSCoordinate(double latitude, double longitude)
    {
        if (latitude < -90d || latitude > 90d)
            throw new ArgumentException(
                "Latitude must be between -90 and 90 degrees.", nameof(latitude));
        if (longitude < -180d || longitude > 180d)
            throw new ArgumentException(
                "Longitude must be between -180 and 180 degrees.", nameof(longitude));
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
