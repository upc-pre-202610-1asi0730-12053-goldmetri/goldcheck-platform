namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record RestartGNSSCommand(string AssetId, string RestartReason);