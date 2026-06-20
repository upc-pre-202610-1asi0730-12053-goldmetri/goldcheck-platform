namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record AnalyseEngineRefrigerantTemperatureCommand(string AssetId, decimal RefrigerantCelsius);