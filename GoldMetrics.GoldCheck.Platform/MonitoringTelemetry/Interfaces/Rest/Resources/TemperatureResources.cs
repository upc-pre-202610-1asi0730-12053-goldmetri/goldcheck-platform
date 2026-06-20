namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

public record MonitorEngineTemperatureResource(string AssetId);

public record TemperatureReadingResource(
    int Id,
    string AssetId,
    string Status,
    decimal? ExhaustCelsius,
    decimal? ExhaustLimitCelsius,
    int? CylinderNumber,
    decimal? RefrigerantCelsius,
    decimal? OilCelsius,
    decimal? FuelCelsius,
    string? AnomalyType,
    decimal? AnomalyCelsius,
    string? AnomalyDescription,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
    
public record AnalyseExhaustTemperatureResource(decimal ExhaustCelsius);
public record AnalyseExhaustLimitPerCylinderResource(decimal LimitCelsius, int CylinderNumber);