using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;

public static class VehicleResourceFromEntityAssembler
{
    public static VehicleResource ToResourceFromEntity(Vehicle entity) =>
        new(entity.Id, entity.VehicleId.Value, entity.OperatorId.Value, entity.IsEngineOn, entity.Status);
}
