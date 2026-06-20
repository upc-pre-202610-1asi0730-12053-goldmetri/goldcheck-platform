namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record ProcessTelemetryDataCommand(string AssetId, string RawData);