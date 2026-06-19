using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;

public static class HaulingCycleResourceFromEntityAssembler
{
    public static HaulingCycleResource ToResourceFromEntity(HaulingCycle entity) =>
        new(
            entity.Id,
            entity.VehicleId.Value,
            entity.LoadingPoint.Name,
            entity.Status,
            entity.RouteProgress);
}
