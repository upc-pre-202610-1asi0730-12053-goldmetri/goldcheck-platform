namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;

public enum ReportingNotificationsError
{
    None,
    ReportNotFound,
    NotificationNotFound,
    InvalidReportFormat,
    ReportAlreadyExported,
    AccidentValidationFailed,
    NotificationValidationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}