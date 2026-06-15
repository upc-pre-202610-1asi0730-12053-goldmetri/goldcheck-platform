using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;

public interface INotificationQueryService
{
    Task<Notification?> Handle(GetNotificationByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Notification>> Handle(GetNotificationsByUserQuery query, CancellationToken cancellationToken);
}