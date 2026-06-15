using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;

public interface IReportQueryService
{
    Task<Report?> Handle(GetReportByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Report>> Handle(GetAllReportsQuery query, CancellationToken cancellationToken);
}