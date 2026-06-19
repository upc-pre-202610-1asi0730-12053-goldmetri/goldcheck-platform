using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Transform;

public static class AssignVehicleCommandFromResourceAssembler
{
    public static AssignVehicleCommand ToCommandFromResource(CreateVehicleResource resource) =>
        new(resource.VehicleId, resource.OperatorId);
}
