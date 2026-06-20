namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record AnalyseEngineOilTemperatureCommand(string AssetId, decimal OilCelsius);