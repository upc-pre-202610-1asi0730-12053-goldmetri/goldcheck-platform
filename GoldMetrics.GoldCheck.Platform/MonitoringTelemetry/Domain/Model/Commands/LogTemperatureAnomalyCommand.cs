namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record LogTemperatureAnomalyCommand(string AssetId, string AnomalyDescription);