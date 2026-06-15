using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Transform;

public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(Notification entity) =>
        new(entity.Id, entity.RecipientId.Value, entity.NotificationType.Value,
            entity.NotificationStatus.Value, entity.Message, entity.Status);
}