using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.Internal.QueryServices;

public class NotificationQueryService(INotificationRepository notificationRepository) : INotificationQueryService
{
    public async Task<Notification?> Handle(GetNotificationByIdQuery query, CancellationToken cancellationToken)
        => await notificationRepository.FindByIdAsync(query.Id, cancellationToken);
    public async Task<IEnumerable<Notification>> Handle(GetNotificationsByUserQuery query, CancellationToken cancellationToken)
        => await notificationRepository.FindByRecipientIdAsync(query.UserId, cancellationToken);
}