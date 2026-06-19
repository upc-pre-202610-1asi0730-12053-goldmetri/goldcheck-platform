namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

public record HaulingCycleResource(
    int Id,
    string VehicleId,
    string LoadingPoint,
    string Status,
    string RouteProgress);
