using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

public interface IGNSSQueryService
{
    Task<IEnumerable<GNSSStatus>> Handle(GetGNSSStatusByAssetQuery query, CancellationToken cancellationToken = default);
}