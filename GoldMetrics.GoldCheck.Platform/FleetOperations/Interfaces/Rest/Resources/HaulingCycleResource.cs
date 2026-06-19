namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

public record HaulingCycleResource(
    int Id,
    string VehicleId,
    string LoadingPoint,
    decimal? Payload,
    string Status,
    string RouteProgress);
