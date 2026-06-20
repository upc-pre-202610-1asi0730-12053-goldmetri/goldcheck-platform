namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

public record MonitorGNSSStatusResource(string AssetId);

public record GNSSStatusResource(
    int Id,
    string AssetId,
    string Status,
    string? RestartReason,
    int RestartCount,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);