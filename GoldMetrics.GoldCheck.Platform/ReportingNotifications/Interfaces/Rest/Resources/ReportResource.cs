namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;

public record ReportResource(
    int Id,
    string SupervisorId,
    string IncidentId,
    string ReportStatus,
    string ReportFormat,
    string Status);