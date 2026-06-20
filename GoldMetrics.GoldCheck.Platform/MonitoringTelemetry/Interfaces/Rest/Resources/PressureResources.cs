namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

public record MonitorPressureResource(string AssetId);
public record AnalysePressureResource(string PressureType, decimal PressureBar);
public record DetectPressureAnomalyResource(string PressureType, decimal PressureBar);

public record PressureReadingResource(
    int Id,
    string AssetId,
    string Status,
    decimal? OilFilterDifferenceBar,
    decimal? OilPanBar,
    decimal? AbsoluteEngineOilBar,
    decimal? OilFilterBar,
    string? AnomalyPressureType,
    string? AnomalyDescription,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);