namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record LogCommunicationAnomalyCommand(string AssetId, string Protocol, string AnomalyDescription);