namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;

public record RequestNotificationCommand(string RecipientId, string Type, string Message);