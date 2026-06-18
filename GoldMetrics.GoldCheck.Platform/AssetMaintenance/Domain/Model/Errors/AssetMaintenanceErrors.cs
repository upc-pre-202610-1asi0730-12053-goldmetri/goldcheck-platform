namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Errors;

public static class AssetMaintenanceErrors
{
    public static AssetMaintenanceError None => AssetMaintenanceError.None;
    public static AssetMaintenanceError OperationCancelled => AssetMaintenanceError.OperationCancelled;
    public static AssetMaintenanceError DatabaseError => AssetMaintenanceError.DatabaseError;
    public static AssetMaintenanceError InternalServerError => AssetMaintenanceError.InternalServerError;
    public static AssetMaintenanceError MachineryNotFound => AssetMaintenanceError.MachineryNotFound;
    public static AssetMaintenanceError InvalidEngineHours => AssetMaintenanceError.InvalidEngineHours;
    public static AssetMaintenanceError MaintenanceAlreadyScheduled => AssetMaintenanceError.MaintenanceAlreadyScheduled;
}