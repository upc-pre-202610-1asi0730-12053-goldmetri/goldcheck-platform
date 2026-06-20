namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record DetectCommunicationAnomalyCommand(string AssetId, string Protocol, string AnomalyDescription);