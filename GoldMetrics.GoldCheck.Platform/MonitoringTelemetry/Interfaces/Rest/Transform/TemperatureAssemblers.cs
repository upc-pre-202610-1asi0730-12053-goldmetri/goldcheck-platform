using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class MonitorEngineTemperatureCommandFromResourceAssembler
{
    public static MonitorEngineTemperatureCommand ToCommandFromResource(MonitorEngineTemperatureResource resource)
        => new(resource.AssetId);
}

public static class TemperatureReadingResourceFromEntityAssembler
{
    public static TemperatureReadingResource ToResourceFromEntity(TemperatureReading entity)
        => new(entity.Id, entity.AssetId.Value, entity.Status,
            entity.ExhaustCelsius, entity.ExhaustLimitCelsius, entity.CylinderNumber,
            entity.RefrigerantCelsius, entity.OilCelsius, entity.FuelCelsius,
            entity.AnomalyType, entity.AnomalyCelsius, entity.AnomalyDescription,
            entity.CreatedAt, entity.UpdatedAt);
    
    public static class AnalyseExhaustCommandFromResourceAssembler
    {
        public static AnalyseExhaustTemperatureCommand ToCommandFromResource(string assetId, AnalyseExhaustTemperatureResource resource)
            => new(assetId, resource.ExhaustCelsius);
    }
    public static class AnalyseExhaustLimitCommandFromResourceAssembler
    {
        public static AnalyseExhaustTemperatureLimitPerCylinderCommand ToCommandFromResource(string assetId, AnalyseExhaustLimitPerCylinderResource resource)
            => new(assetId, resource.LimitCelsius, resource.CylinderNumber);
    }
}