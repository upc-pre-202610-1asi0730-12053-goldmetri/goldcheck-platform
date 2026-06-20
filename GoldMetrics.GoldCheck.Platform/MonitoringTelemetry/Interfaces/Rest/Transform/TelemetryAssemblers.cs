using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class ProcessTelemetryCommandFromResourceAssembler
{
    public static ProcessTelemetryDataCommand ToCommandFromResource(ProcessTelemetryDataResource resource)
        => new(resource.AssetId, resource.RawData);
}

public static class TelemetryDataResourceFromEntityAssembler
{
    public static TelemetryDataResource ToResourceFromEntity(TelemetryData entity)
        => new(entity.Id, entity.AssetId.Value, entity.TelemetryDataId,
            entity.RawData, entity.Status, entity.IsValidated,
            entity.CreatedAt, entity.UpdatedAt);
}