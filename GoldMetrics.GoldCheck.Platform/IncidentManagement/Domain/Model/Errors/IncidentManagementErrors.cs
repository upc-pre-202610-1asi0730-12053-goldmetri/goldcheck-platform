namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Errors;

public static class IncidentManagementErrors
{
    public static IncidentManagementError None => IncidentManagementError.None;
    public static IncidentManagementError IncidentNotFound => IncidentManagementError.IncidentNotFound;
    public static IncidentManagementError InvalidRiskLevel => IncidentManagementError.InvalidRiskLevel;
    public static IncidentManagementError InvalidIncidentType => IncidentManagementError.InvalidIncidentType;
    public static IncidentManagementError IncidentAlreadyCommitted => IncidentManagementError.IncidentAlreadyCommitted;
    public static IncidentManagementError RiskLevelValidationFailed => IncidentManagementError.RiskLevelValidationFailed;
    public static IncidentManagementError OperationCancelled => IncidentManagementError.OperationCancelled;
    public static IncidentManagementError DatabaseError => IncidentManagementError.DatabaseError;
    public static IncidentManagementError InternalServerError => IncidentManagementError.InternalServerError;
}