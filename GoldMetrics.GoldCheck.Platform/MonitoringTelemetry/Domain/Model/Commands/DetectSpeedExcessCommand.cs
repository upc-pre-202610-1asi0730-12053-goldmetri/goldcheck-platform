namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record DetectSpeedExcessCommand(string AssetId, decimal SpeedKmPerHour, decimal SpeedLimitKmPerHour);