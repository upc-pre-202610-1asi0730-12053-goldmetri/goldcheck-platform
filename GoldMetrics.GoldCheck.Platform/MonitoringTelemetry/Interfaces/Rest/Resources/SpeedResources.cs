namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

public record MonitorSpeedStatusResource(string AssetId);
public record DetectSpeedExcessResource(decimal SpeedKmPerHour, decimal SpeedLimitKmPerHour);

public record SpeedReadingResource(
    int Id,
    string AssetId,
    string Status,
    decimal? CurrentSpeedKmPerHour,
    decimal? SpeedLimitKmPerHour,
    bool IsViolation,
    string? ViolationDescription,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);