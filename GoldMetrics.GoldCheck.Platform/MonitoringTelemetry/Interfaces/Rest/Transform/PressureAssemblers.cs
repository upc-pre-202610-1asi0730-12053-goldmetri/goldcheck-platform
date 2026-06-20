using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class MonitorPressureCommandFromResourceAssembler
{
    public static MonitorPressureCommand ToCommandFromResource(MonitorPressureResource resource)
        => new(resource.AssetId);
}

public static class PressureReadingResourceFromEntityAssembler
{
    public static PressureReadingResource ToResourceFromEntity(PressureReading entity)
        => new(entity.Id, entity.AssetId.Value, entity.Status,
            entity.OilFilterDifferenceBar, entity.OilPanBar,
            entity.AbsoluteEngineOilBar, entity.OilFilterBar,
            entity.AnomalyPressureType, entity.AnomalyDescription,
            entity.CreatedAt, entity.UpdatedAt);
}