namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;

public record RequestAccidentDataCommand(string IncidentId, string SupervisorId);