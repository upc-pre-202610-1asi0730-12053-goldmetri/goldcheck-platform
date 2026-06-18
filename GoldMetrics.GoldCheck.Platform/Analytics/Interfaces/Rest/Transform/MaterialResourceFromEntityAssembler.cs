using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Transform;

public static class MaterialResourceFromEntityAssembler
{
    public static MaterialResource ToResourceFromEntity(Material entity) =>
        new(
            entity.Id,
            entity.MaterialId.Value,
            entity.RouteId.Value,
            entity.RouteStatus.Value,
            entity.SupervisorId.Value,
            entity.UserId.Value,
            entity.ProductionStart, 
            entity.ProductionEnd,
            entity.ProductionTons.Value,
            entity.Status
            );
}