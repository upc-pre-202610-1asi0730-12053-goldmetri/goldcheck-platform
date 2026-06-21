namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Errors;

public enum IncidentManagementError
{
    None,
    IncidentNotFound,
    InvalidRiskLevel,
    InvalidIncidentType,
    IncidentAlreadyCommitted,
    RiskLevelValidationFailed,
    MachineryNotFound,
    OperatorNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}