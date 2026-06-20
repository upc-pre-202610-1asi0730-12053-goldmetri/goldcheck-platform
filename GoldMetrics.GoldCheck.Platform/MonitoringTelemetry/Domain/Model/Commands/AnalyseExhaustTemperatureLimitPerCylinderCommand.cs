namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record AnalyseExhaustTemperatureLimitPerCylinderCommand(string AssetId, decimal LimitCelsius, int CylinderNumber);