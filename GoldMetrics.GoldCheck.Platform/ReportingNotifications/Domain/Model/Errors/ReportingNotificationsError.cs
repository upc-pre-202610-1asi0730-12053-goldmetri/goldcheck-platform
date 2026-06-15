namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;

public enum ReportingNotificationsError
{
    None,
    ReportNotFound,
    InvalidReportFormat,
    ReportAlreadyExported,
    AccidentValidationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}