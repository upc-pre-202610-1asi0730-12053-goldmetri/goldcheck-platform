namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;

public record RequestNotificationResource(string RecipientId, string Type, string Message);