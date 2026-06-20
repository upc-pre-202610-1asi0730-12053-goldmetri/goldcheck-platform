using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class MonitorSpeedCommandFromResourceAssembler
{
    public static MonitorSpeedStatusCommand ToCommandFromResource(MonitorSpeedStatusResource resource)
        => new(resource.AssetId);
}

public static class SpeedReadingResourceFromEntityAssembler
{
    public static SpeedReadingResource ToResourceFromEntity(SpeedReading entity)
        => new(entity.Id, entity.AssetId.Value, entity.Status,
            entity.CurrentSpeedKmPerHour, entity.SpeedLimitKmPerHour, entity.IsViolation,
            entity.ViolationDescription, entity.CreatedAt, entity.UpdatedAt);
}

public static class DetectSpeedExcessCommandFromResourceAssembler
{
    public static DetectSpeedExcessCommand ToCommandFromResource(string assetId, DetectSpeedExcessResource resource)
        => new(assetId, resource.SpeedKmPerHour, resource.SpeedLimitKmPerHour);
}