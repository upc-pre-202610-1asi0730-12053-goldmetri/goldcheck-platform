namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;

public record NotificationResource(
    int Id,
    string RecipientId,
    string NotificationType,
    string NotificationStatus,
    string Message,
    string Status);