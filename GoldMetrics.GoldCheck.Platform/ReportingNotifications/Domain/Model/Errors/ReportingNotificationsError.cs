namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;

public enum ReportingNotificationsError
{
    None,
    ReportNotFound,
    AccidentValidationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}