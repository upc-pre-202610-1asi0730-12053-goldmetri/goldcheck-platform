namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Errors;

public enum AssetMaintenanceError
{
    None,
    MachineryNotFound,
    ComponentNotFound,
    MachineryAlreadyDischarged,
    InvalidEngineHours,
    MaintenanceAlreadyScheduled,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}