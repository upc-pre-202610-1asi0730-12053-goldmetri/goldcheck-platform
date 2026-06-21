namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model;

public enum FleetOperationsError
{
    None,
    VehicleNotFound,
    VehicleAlreadyAssigned,
    MachineryNotFound,
    HaulingCycleNotFound,
    InvalidPayload,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
