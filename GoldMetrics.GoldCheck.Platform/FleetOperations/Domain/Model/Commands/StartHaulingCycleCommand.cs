namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;

public record StartHaulingCycleCommand(string VehicleId, string LoadingPoint);
