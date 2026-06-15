using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Errors;
    using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Repositories;
    using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
    using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
    using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;

    namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.Internal.CommandServices;

    public class NotificationCommandService(
        INotificationRepository notificationRepository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<ErrorMessages> localizer)
        : INotificationCommandService
    {
        private Result<Notification> NotFound() =>
            Result<Notification>.Failure(ReportingNotificationsError.NotificationNotFound, localizer[nameof(ReportingNotificationsError.NotificationNotFound)]);
        private Result<Notification> Cancelled() =>
            Result<Notification>.Failure(ReportingNotificationsError.OperationCancelled, localizer[nameof(ReportingNotificationsError.OperationCancelled)]);
        private Result<Notification> DbError() =>
            Result<Notification>.Failure(ReportingNotificationsError.DatabaseError, localizer[nameof(ReportingNotificationsError.DatabaseError)]);
        private Result<Notification> ServerError() =>
            Result<Notification>.Failure(ReportingNotificationsError.InternalServerError, localizer[nameof(ReportingNotificationsError.InternalServerError)]);

        public async Task<Result<Notification>> Handle(RequestNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = new Notification(command);
            try
            {
                await notificationRepository.AddAsync(notification, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<Notification>.Success(notification);
            }
            catch (ArgumentException)
            {
                return Result<Notification>.Failure(ReportingNotificationsError.NotificationValidationFailed, localizer[nameof(ReportingNotificationsError.NotificationValidationFailed)]);
            }
            catch (OperationCanceledException) { return Cancelled(); }
            catch (DbUpdateException) { return DbError(); }
            catch (Exception) { return ServerError(); }
        }
    }