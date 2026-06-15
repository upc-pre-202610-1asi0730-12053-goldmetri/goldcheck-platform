using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Events;

public record NotificationRequestedEvent(string NotificationId, string RecipientId, string Type) : IEvent;