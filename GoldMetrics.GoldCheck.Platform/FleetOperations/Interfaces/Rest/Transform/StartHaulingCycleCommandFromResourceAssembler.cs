using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;

public static class StartHaulingCycleCommandFromResourceAssembler
{
    public static StartHaulingCycleCommand ToCommandFromResource(StartHaulingCycleResource resource) =>
        new(resource.VehicleId, resource.LoadingPoint);
}
