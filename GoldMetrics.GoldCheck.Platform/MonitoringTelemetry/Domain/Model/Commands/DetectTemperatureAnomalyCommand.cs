namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record DetectTemperatureAnomalyCommand(string AssetId, string AnomalyType, decimal AnomalyCelsius);