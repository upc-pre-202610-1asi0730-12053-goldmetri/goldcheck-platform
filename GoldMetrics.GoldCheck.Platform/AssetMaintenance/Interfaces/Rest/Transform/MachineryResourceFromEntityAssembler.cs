using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest.Transform;

public static class MachineryResourceFromEntityAssembler
{
    public static MachineryResource ToResourceFromEntity(Machinery entity) =>
        new(entity.Id, entity.MachineryId.Value, entity.Model, entity.OEM,
            entity.EngineHours.Hours, entity.MaintenanceStatus.Value,
            entity.MaintenanceScheduledAtHours, entity.DischargeReason,
            entity.DischargedComponentId, entity.ComponentDischargeReason, entity.Status);
}