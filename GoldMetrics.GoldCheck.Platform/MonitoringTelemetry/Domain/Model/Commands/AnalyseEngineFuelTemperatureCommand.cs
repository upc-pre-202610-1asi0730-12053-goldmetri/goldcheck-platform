namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record AnalyseEngineFuelTemperatureCommand(string AssetId, decimal FuelCelsius);