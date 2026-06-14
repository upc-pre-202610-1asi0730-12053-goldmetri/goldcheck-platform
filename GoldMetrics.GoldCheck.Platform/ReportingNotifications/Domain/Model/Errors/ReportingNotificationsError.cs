namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;

public enum ReportingNotificationsError
{
    None,
    ReportNotFound,
    InvalidReportFormat,
    AccidentValidationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}