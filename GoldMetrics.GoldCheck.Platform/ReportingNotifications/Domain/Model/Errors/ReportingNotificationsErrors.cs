namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;

public static class ReportingNotificationsErrors
{
    public static ReportingNotificationsError None => ReportingNotificationsError.None;
    public static ReportingNotificationsError ReportNotFound => ReportingNotificationsError.ReportNotFound;
    public static ReportingNotificationsError InvalidReportFormat => ReportingNotificationsError.InvalidReportFormat;
    public static ReportingNotificationsError ReportAlreadyExported => ReportingNotificationsError.ReportAlreadyExported;
    public static ReportingNotificationsError AccidentValidationFailed => ReportingNotificationsError.AccidentValidationFailed;
    public static ReportingNotificationsError OperationCancelled => ReportingNotificationsError.OperationCancelled;
    public static ReportingNotificationsError DatabaseError => ReportingNotificationsError.DatabaseError;
    public static ReportingNotificationsError InternalServerError => ReportingNotificationsError.InternalServerError;
}