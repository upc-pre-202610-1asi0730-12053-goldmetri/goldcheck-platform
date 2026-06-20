namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record ValidateTelemetryDataCommand(string AssetId, string TelemetryDataId);