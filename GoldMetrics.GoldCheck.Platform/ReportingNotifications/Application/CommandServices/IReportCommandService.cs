using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;

public interface IReportCommandService
{
    Task<Result<Report>> Handle(RequestAccidentDataCommand command, CancellationToken cancellationToken);
    Task<Result<Report>> Handle(LoadAccidentDataCommand command, CancellationToken cancellationToken);
    Task<Result<Report>> Handle(GenerateReportCommand command, CancellationToken cancellationToken);
    Task<Result<Report>> Handle(RequestReportExportationCommand command, CancellationToken cancellationToken);
    Task<Result<Report>> Handle(ExportReportCommand command, CancellationToken cancellationToken);
}