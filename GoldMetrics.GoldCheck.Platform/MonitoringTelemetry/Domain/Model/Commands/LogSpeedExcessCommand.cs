namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record LogSpeedExcessCommand(string AssetId, decimal SpeedKmPerHour);