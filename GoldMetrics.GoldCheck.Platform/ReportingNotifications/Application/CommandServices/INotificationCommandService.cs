using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;

public interface INotificationCommandService
{
    Task<Result<Notification>> Handle(RequestNotificationCommand command, CancellationToken cancellationToken);
    Task<Result<Notification>> Handle(SendNotificationCommand command, CancellationToken cancellationToken);
}