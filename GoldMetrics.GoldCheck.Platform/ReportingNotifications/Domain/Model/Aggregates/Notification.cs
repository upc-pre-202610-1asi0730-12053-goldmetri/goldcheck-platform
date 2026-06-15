using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;

public partial class Notification
{
    public Notification()
    {
        RecipientId = new RecipientId();
        NotificationType = new NotificationType();
        NotificationStatus = new NotificationStatus();
        Message = string.Empty;
        Status = string.Empty;
    }

    public Notification(RequestNotificationCommand command)
    {
        RecipientId = new RecipientId(command.RecipientId);
        NotificationType = new NotificationType(command.Type);
        Message = command.Message;
        NotificationStatus = new NotificationStatus("Requested");
        Status = "NotificationRequested";
    }

    public int Id { get; }
    public RecipientId RecipientId { get; private set; }
    public NotificationType NotificationType { get; private set; }
    public NotificationStatus NotificationStatus { get; private set; }
    public string Message { get; private set; }
    public string Status { get; private set; }
}