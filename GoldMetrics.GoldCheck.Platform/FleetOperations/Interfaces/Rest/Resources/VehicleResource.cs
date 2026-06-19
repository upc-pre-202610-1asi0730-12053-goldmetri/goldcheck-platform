namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Rest.Resources;

public record VehicleResource(int Id, string VehicleId, string OperatorId, bool IsEngineOn, string Status);
