using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

public interface ITelemetryQueryService
{
    Task<IEnumerable<TelemetryData>> Handle(GetTelemetryDataByAssetQuery query, CancellationToken cancellationToken = default);
}