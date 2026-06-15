using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.Internal.QueryServices;

public class ReportQueryService(IReportRepository reportRepository) : IReportQueryService
{
    public async Task<Report?> Handle(GetReportByIdQuery query, CancellationToken cancellationToken)
        => await reportRepository.FindByIdAsync(query.Id, cancellationToken);
    public async Task<IEnumerable<Report>> Handle(GetAllReportsQuery query, CancellationToken cancellationToken)
        => await reportRepository.ListAsync(cancellationToken);
}