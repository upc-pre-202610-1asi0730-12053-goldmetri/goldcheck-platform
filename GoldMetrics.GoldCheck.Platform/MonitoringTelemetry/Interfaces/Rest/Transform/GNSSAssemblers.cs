using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class MonitorGNSSCommandFromResourceAssembler
{
    public static MonitorGNSSStatusCommand ToCommandFromResource(MonitorGNSSStatusResource resource)
        => new(resource.AssetId);
}

public static class GNSSStatusResourceFromEntityAssembler
{
    public static GNSSStatusResource ToResourceFromEntity(GNSSStatus entity)
        => new(entity.Id, entity.AssetId.Value, entity.Status,
            entity.RestartReason, entity.RestartCount,
            entity.CreatedAt, entity.UpdatedAt);
}


public static class RestartGNSSCommandFromResourceAssembler
{
    public static RestartGNSSCommand ToCommandFromResource(string assetId, RestartGNSSResource resource)
        => new(assetId, resource.RestartReason);
}