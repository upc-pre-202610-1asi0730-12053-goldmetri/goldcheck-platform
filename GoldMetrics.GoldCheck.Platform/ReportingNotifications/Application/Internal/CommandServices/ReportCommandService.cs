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

public class ReportCommandService(
    IReportRepository reportRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IReportCommandService
{
    private async Task<Report?> FindReport(int id, CancellationToken ct)
        => await reportRepository.FindByIdAsync(id, ct);

    private Result<Report> NotFound() =>
        Result<Report>.Failure(ReportingNotificationsError.ReportNotFound, localizer[nameof(ReportingNotificationsError.ReportNotFound)]);
    private Result<Report> Cancelled() =>
        Result<Report>.Failure(ReportingNotificationsError.OperationCancelled, localizer[nameof(ReportingNotificationsError.OperationCancelled)]);
    private Result<Report> DbError() =>
        Result<Report>.Failure(ReportingNotificationsError.DatabaseError, localizer[nameof(ReportingNotificationsError.DatabaseError)]);
    private Result<Report> ServerError() =>
        Result<Report>.Failure(ReportingNotificationsError.InternalServerError, localizer[nameof(ReportingNotificationsError.InternalServerError)]);

    public async Task<Result<Report>> Handle(RequestAccidentDataCommand command, CancellationToken cancellationToken)
    {
        var report = new Report(command);
        try
        {
            await reportRepository.AddAsync(report, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Report>.Success(report);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }

    public async Task<Result<Report>> Handle(LoadAccidentDataCommand command, CancellationToken cancellationToken)
    {
        var report = await FindReport(command.Id, cancellationToken);
        if (report is null) return NotFound();
        try
        {
            report.LoadAccidentData(command);
            reportRepository.Update(report);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Report>.Success(report);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
}
