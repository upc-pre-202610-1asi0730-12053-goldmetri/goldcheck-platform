namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record LogPressureAnomalyCommand(string AssetId, string AnomalyDescription);