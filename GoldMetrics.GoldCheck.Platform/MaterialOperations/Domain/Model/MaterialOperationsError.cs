namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model;

public enum MaterialOperationsError
{
    None,
    MaterialNotFound,
    InvalidMineralType,
    InvalidPayload,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
