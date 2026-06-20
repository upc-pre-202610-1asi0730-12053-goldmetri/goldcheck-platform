namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record AnalyseExhaustTemperatureCommand(string AssetId, decimal ExhaustCelsius);